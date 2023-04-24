# Utilizando secuencias (sequences) en PostgreSQL con C#

Con frecuencia en un buen diseño de bases de datos es necesario que las tablas tengan al menos un identificador único para poderlas relacionar con otras, siguiendo las recomendaciones de las conocidas reglas de Codd para el modelo relacional de bases de datos.Para cumplir con este requerimiento podemos fijarnos en la entidad que representa cada tabla y seguir la especificación del identificador único que le correspondería según nos dicten las reglas del negocio o el tipo de sistema que vamos a construir.

Hay casos en donde la tabla no tiene una entidad definida sino sirve únicamente como tabla de soporte para una relación muchos a muchos, como un catálogo o un listado de parámetros para el sistema, en estos casos donde no se tiene una especificación definida para asignar un identificador único lo más recomendado es utilizar una serie o un contador como valor para ese identificador.

PostgreSQL proporciona unos objetos llamados secuencias (sequences) que sirven para crear contadores o series, las secuencias son objetos de bases de datos al mismo nivel que las tablas, vistas, triggers o funciones.

Aunque pueden crearse contadores y series de forma manual esto no será tan eficiente como los objetos sequence que nos proporciona PostgreSQL ya que mejoran el desempeño de la base de datos sobretodo en sistemas multiusuario. La forma automática de crear una secuencia es utilizar el tipo de dato serial en una columna, como se muestra a continuación con el siguiente script para una tabla llamada Publishers.



Al ejecutar este script se crean dos objetos: la secuencia (sequence) y la tabla (en ese orden), como lo muestra la pestaña messages de pgadmin al finalizar la ejecución del script.



Ahora creamos una función plpgsql con la que agregaremos los registros a la tabla, en esta función establecemos los valores mediante parámetros para cada una de las columnas, excepto claro el identificador (columna publisherid), ya que de ese valor se encargará la secuencia.



Ahora con el siguiente programa en C# probaremos la secuencia agregando algunos registros e imprimiendo sus identificadores en la consola.



Compilamos el programa con el siguiente comando.

$ gmcs -r:/home/martin/lib/Npgsql2.0.11.94/Mono2.0/bin/Npgsql.dll,System.Data Main.cs


Al ejecutar el programa, se mostrarán los registros agregados y los valores que les asigno la secuencia como identificador.



También podemos crear una secuencia (sequence) de forma manual siguiendo la sintaxis:

create sequence [name] start with [number] increment by [number]
De manera predeterminada las secuencias comienzan en el número 1 al menos no se indique con el comando start with. Por ejemplo creamos una secuencia que se incremente de 6 en 6

Test=# CREATE SEQUENCE my_first_sequence_seq INCREMENT BY 6;
Para acceder a los valores de la secuencia lo hacemos con un SELECT y la función nextval() como se muestra a continuación:



Observamos que la secuencia comienza en 1 e incrementa de 6 en 6.
Para acceder al valor actual de la secuencia lo hacemos con la función currval() como se muestra a continuación:



Para establecer un nuevo valor en la secuencia utilizamos la función setval()



Por último podemos eliminar la secuencia creada con el comando:

DROP SEQUENCE [name]
Como se muestra a continuación, en la siguiente imagen:


