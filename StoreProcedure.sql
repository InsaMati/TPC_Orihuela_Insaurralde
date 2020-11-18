use TP_FINAL

-- LISTAR ARTICULO
create procedure SP_Listar

-- AGREGAR ARTICULO
create procedure SP_Agregar_Articulo(
@Codigo varchar (50),
@Nombre varchar (50),
@Descripcion varchar (150),
@Marca smallint,
@Categoria smallint,
@Imagen varchar (1000),
@Precio money,
@Stock smallint,
@Estado bit
)
as
BEGIN TRY 
	INSERT INTO ARTICULOS (Codigo,Nombre,Descripcion,IdMarca,IdCategoria,ImagenURL,Precio,Stock,Estado)
	VALUES (@Codigo,@Nombre,@Descripcion,@Marca,@Categoria,@Imagen,@Precio,@Stock,@Estado)
END TRY
BEGIN CATCH
	RAISERROR('Error al Agregar Articulo',16,1)
END CATCH
go
-- MODIFICAR ARTICULO
create procedure SP_Modificar_Articulo(
@ID  smallint,
@Codigo varchar (50),
@Nombre varchar (50),
@Descripcion varchar (150),
@Marca smallint,
@Categoria smallint,
@Imagen varchar (1000),
@Precio money,
@Stock smallint,
@Estado bit
)
as
BEGIN TRY 
	UPDATE  ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @Marca, IdCategoria = @Categoria, ImagenURL = @Imagen, Precio = @Precio,
	Stock = @Stock, Estado = @Estado where ID = @ID
END TRY
BEGIN CATCH
	RAISERROR('Error al Modificar Articulo',16,1)
END CATCH
GO

-- LISTAR USUARIOS
create procedure SP_ListarUsuarios(@Tipo smallint) as
BEGIN TRY
	SELECT * FROM USUARIO
	WHERE USUARIO.IdTipoUsuario = @Tipo
END TRY
BEGIN CATCH
	RAISERROR('ERROR AL LISTAR ALUMNOS POR TIPO',16,1)
END CATCH

-- AGREGAR USUARIO

create procedure SP_AgregarUsuario(
@Email varchar(100),
@Contraseņa varchar(100),
@IdTipoUsuario smallint
) AS
BEGIN TRY
	
		INSERT INTO USUARIO (Email,Contraseņa,IdTipoUsuario,Estado)
		VALUES (@Email,@Contraseņa,@IdTipoUsuario,1)
		
END TRY
BEGIN CATCH
	RAISERROR('Error al registrar un usuario nuevo',16,1)
END CATCH



create procedure SP_AgregarDatosPersonales
(
@IdUsuario smallint,
@Nombre varchar(50),
@Apellido varchar(50),
@Dni int,
@Genero smallint,
@Direccion varchar(100),
@Provincia smallint,
@FechaNacimiento date,
@Telefono bigint
)
AS
BEGIN TRY
        INSERT INTO DATOS_PERSONALES (ID_usuario,Nombre,Apellido,Dni,ID_Genero,Direccion,ID_Provincia,Fecha_Nac,Telefono)
		VALUES (@IdUsuario,@Nombre,@Apellido,@Dni,@Genero,@Direccion,@Provincia,@FechaNacimiento,@Telefono)
		END TRY
BEGIN CATCH
	RAISERROR('Error al registrar datos de un nuevo usuario.',16,1)
END CATCH


-- AGREGAR VENTA                 PROTOTIPO MEDIO TRUCHO
create procedure SP_ComprarCarrito(
	@IdUsuario smallint,
	@CostoTotal money
) as
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			insert into FACTURA (ID_pedido, ID_usuario, Fecha, Forma_de_pago, Importe)
			values (@IdUsuario,@CostoTotal)
			declare @idcarrito SMALLINT
			select @idcarrito = c.ID FROM CARRITO as c
			inner join pedido as p on p.ID_carrito = c.ID
			insert into PEDIDO (ID_carrito,ID_estado,Fecha)
			values(@idcarrito,1,getdate())
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		RAISERROR('ERROR AL CARGAR UN PEDIDO',16,1)
	END CATCH
END



create procedure SP_VERIFICAR_USUARIO
(
@Email varchar(100),
@Contraseņa varchar(100)
) as
BEGIN TRY
select ID, Email, Contraseņa, IdTipoUsuario, Estado from USUARIO where Email = @Email and Contraseņa=@Contraseņa
END TRY
BEGIN CATCH
RAISERROR('Usuario Inexistente.',16,1)
END CATCH

select *From USUARIO
select *from DATOS_PERSONALES


-- AGREGAR USUARIO
/*
create procedure SP_AgregarUsuario(

@Email varchar(100),
@Contraseņa varchar(100),
@IdTipoUsuario smallint,
@Nombre varchar(50),
@Apellido varchar(50),
@Dni smallint,
@Genero smallint,
@Direccion varchar(100),
@Provincia smallint,
@FechaNacimiento date
) AS
BEGIN TRY
	BEGIN TRANSACTION
		INSERT INTO USUARIO (Email,Contraseņa,IdTipoUsuario,Estado)
		VALUES (@Email,@Contraseņa,@IdTipoUsuario,1)
		INSERT INTO DATOS_PERSONALES (Nombre,Apellido,Dni,ID_Genero,Direccion,ID_Provincia,Fecha_Nac)
		VALUES (@Nombre,@Apellido,@Dni,@Genero,@Direccion,@Provincia,@FechaNacimiento)
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	RAISERROR('ERROR AL CARGAR UN USUARIO',16,1)
END CATCH

*/