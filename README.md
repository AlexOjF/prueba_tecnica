# 🧑🏾‍💻 Tech Interview

Owner: Alex David Orjuela Floréz
Tags: Codebase

# Creando el modelo de entidad-relación

Para este caso se ven las siguientes relaciones que son brindadas en el ejercicio:

> **Campos aspirante:**
> 
> 1. Número de documento de identidad
> 2. Nombres y Apellidos
> 3. Teléfono Celular
> 4. Correo electrónico
> 
> **Campos Programa académico**
> 
> 1. Código único
> 2. Descripción o nombre del programa.
> 3. Nivel académico [Pregrado, Posgrado]
> 
> **Campos Matricula**
> 
> Deberán ser definidos por el candidato, y ser consistentes con los campos ya conocidos.
> 

En este caso para el campo de Matricula, lo que propongo crear los atributos de `fecha_inscripción`, `limite_pago` , `costo_matricula` y pago (este atributo sería para referenciar si ya pago o está pendiente de cancelar la matricula). Para poder relacionar las otras dos tablas, como se expresa en el siguiente esquema:

![Diagrama Entidad Relación](https://i.imgur.com/FS0061h.png)

# Creando los scripts para la creación de la base de datos y las tablas

Teniendo en cuenta el esquema anterior lo que se dispone a hacer ahora es colocar la información de los script generados para cada tabla:

### **Tabla Aspirante:**

```sql
CREATE TABLE Aspirante (
  ID VARCHAR(13) PRIMARY KEY,
  Nombre TEXT NOT NULL,
  Apellido TEXT NOT NULL,
  Celular VARCHAR(13) NOT NULL,
  Correo TEXT NOT NULL,
  UNIQUE (ID)
);
```

### Tabla **Programa**:

```sql
CREATE TABLE Programa (
  ID VARCHAR(30) PRIMARY KEY,
  Descripción TEXT NOT NULL,
  Nivel VARCHAR(20) NOT NULL
);
```

### Tabla Matrícula:

```sql
CREATE TABLE Matricula (
  ID INT AUTO_INCREMENT PRIMARY KEY,
  ID_Aspirante VARCHAR(13) NOT NULL,
  ID_Programa VARCHAR(30) NOT NULL,
  Fecha_Incripción DATETIME NOT NULL,
  Limite_Pago DATETIME NOT NULL,
  Costo_Matricula DOUBLE NOT NULL,
  Pago BOOL NOT NULL,
  FOREIGN KEY (ID_Aspirante) REFERENCES Aspirante(ID),
  FOREIGN KEY (ID_Programa) REFERENCES Programa(ID)
);
```

### Script para crear las tablas en la base de datos y data de ejemplo.

```sql
CREATE DATABASE pruebas;
USE pruebas;

DROP TABLE IF EXISTS Matricula;
DROP TABLE IF EXISTS Aspirante;
DROP TABLE IF EXISTS Programa;

CREATE TABLE Aspirante (
  ID VARCHAR(13) PRIMARY KEY,
  Nombre TEXT NOT NULL,
  Apellido TEXT NOT NULL,
  Celular VARCHAR(13) NOT NULL,
  Correo TEXT NOT NULL
);

CREATE TABLE Programa (
  ID VARCHAR(30) PRIMARY KEY,
  Descripción TEXT NOT NULL,
  Nivel VARCHAR(20) NOT NULL
);

CREATE TABLE Matricula (
  ID INT AUTO_INCREMENT PRIMARY KEY,
  ID_Aspirante VARCHAR(13) NOT NULL,
  ID_Programa VARCHAR(30) NOT NULL,
  Fecha_Incripción DATETIME NOT NULL,
  Limite_Pago DATETIME NOT NULL,
  Costo_Matricula DOUBLE NOT NULL,
  Pago BOOL NOT NULL,
  FOREIGN KEY (ID_Aspirante) REFERENCES Aspirante(ID),
  FOREIGN KEY (ID_Programa) REFERENCES Programa(ID)
);

-- Insertando datos en la tabla Programa
INSERT INTO Programa (ID, Descripción, Nivel) VALUES
('Prog001', 'Ingeniería de Sistemas', 'Universitario'),
('Prog002', 'Biología', 'Universitario'),
('Prog003', 'Literatura', 'Posgrado');

-- Insertando datos en la tabla Aspirante
INSERT INTO Aspirante (ID, Nombre, Apellido, Celular, Correo) VALUES
('ASPR0001', 'Laura', 'García', '3123456789', 'laura.garcia@example.com'),
('ASPR0002', 'Carlos', 'Pérez', '3209876543', 'carlos.perez@example.com'),
('ASPR0003', 'Ana', 'Lopez', '3151234567', 'ana.lopez@example.com');

-- Insertando datos en la tabla Matricula
INSERT INTO Matricula (ID_Aspirante, ID_Programa, Fecha_Incripción, Limite_Pago, Costo_Matricula, Pago) VALUES
('ASPR0001', 'Prog001', '2023-01-15 00:00:00', '2023-02-15 23:59:59', 2500000.50, TRUE),
('ASPR0002', 'Prog002', '2023-02-01 00:00:00', '2023-03-01 23:59:59', 1500000.00, FALSE),
('ASPR0003', 'Prog003', '2023-03-20 00:00:00', '2023-04-20 23:59:59', 3000000.75, TRUE);

```

Es necesario realizar un proceso para poder otorgar los permisos de conexión:

```sql
CREATE USER 'user_app'@'%' IDENTIFIED BY 'Elfenix11tsm*';
GRANT USAGE ON *.* TO 'user_app'@'%';
FLUSH PRIVILEGES;

```

```sql
GRANT SELECT, INSERT, UPDATE, DELETE ON prueba.* TO 'user_app'@'%';
FLUSH PRIVILEGES;
```

```sql
GRANT ALL PRIVILEGES ON pruebas.* TO 'user_app'@'%';
FLUSH PRIVILEGES;
```

Para poder brindar accesos a un usuario que pueda consultar desde cualquier red se usa el simbolo `%` , este sirve para informar a Mysql que puede tener conexiones desde cualquier punto. No se recomienda para producción.

# Creación docker mysql

### Comandos para instanciar el contenedor de docker de mysql

Este comando permite crear el contenedor y publicarlo para poder conectarse:

```docker
docker run --name=mysql1 --restart on-failure -e MYSQL_ROOT_HOST="%" -p 3306:3306 -d container-registry.oracle.com/mysql/community-server:latest
```

Luego de eso es necesario ingresar al contenedor para eso debe buscarse primero la contraseña que genera por defecto al instanciarse, para obtener esa contraseña se debe usar el siguiente comando:

```docker
docker logs mysql1 2>&1 | grep GENERATED
```

Para conectarse mediante shell a mysql se usa:

```docker
docker exec -it mysql1 mysql -uroot -p
```

Una vez dentro del mysql, se debe introducir la contraseña anteriormente encontrada e ingresar.

Para antes poder hacer alguna consulta dentro del mysql se debe cambiar la contraseña a una que sea de nuestro agrado.

```sql
mysql> ALTER USER 'root'@'localhost' IDENTIFIED BY 'password';
Para este caso la contraseña debe ser la misma que la del connection String que se usará más adelante
```

# Creación clases y cadena de conexión base de datos

Se debe primero realizar un scaffold de la base de datos creada con anterioridad y para eso se usa el siguiente comando:

```sql
Scaffold-DbContext "server=localhost;port=3306;user=user_app;password=Elfenix11tsm*;database=pruebas" MySql.EntityFrameworkCore -OutputDir Entities -f

```

# **Establecer los controladores y sus métodos para el servicio web.**

Con el objetivo de transportar los datos entre los procesos para el manejo de la base de datos y los procesos para trabajar con los servicios web, es recomendable establecer clases DTO por cada entidad del proyecto, en este caso, un DTO para las entidades `Aspirante` , `Programa` y `Matricula`.

# **Controladores para el Web API.**

Ahora lo que haremos es agregar los controladores, en este caso el controlador para el usuario, el cual permitirá establecer métodos para realizar operaciones CRUD sobre las tablas de la base de datos y exponerlos a través del Web API. 

Se realizaría el controlador para cada entidad debido a las tablas generadas anteriormente. Creando así `AspiranteController` , `ProgramaController` y `MatriculaController`.

# Ejecutar la aplicación.

Se de ingresar al siguinete repositorio: 

[https://gitlab.com/prueba7364513/crud_universidad](https://gitlab.com/prueba7364513/crud_universidad)

Ejecutar con la versión en http, en el IDE.

![Botón ejecución](https://i.imgur.com/JiU1xzi.png)

Puede salir el siguiente error:

![Mensaje error](https://i.imgur.com/TgO40L4.png)

Dar clic en yes. Esto levantará un swagger dónde se podrán usar los métodos válidos de la API.