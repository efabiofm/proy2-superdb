SuperDB inicializa con sin ninguna referencia a una base de datos, al primer arranque se debe crear una usando superDB init <nombre_base>, lo cual crear� los registros necesarios y un archivo text en la ruta C:\ con la referencia a la �ltima base de datos creada, para que al volver a inicializar la aplicaci�n, sea dicha base de la que lea informaci�n.

Comandos b�sicos:

superDB init <nombre_base> -------> Crea una nueva base de datos.
superDB show <nombre_tabla> ------> Mostrar los registros de una tabla en la base de datos.
superDB select <id_usuario> ------> Mostrar los datos, puntos de contacto y mascotas de un usuario.
superDB delete <nombre_base> -----> Eliminar la base de datos indicada.
superDB clear 0 ------------------> Limpiar la consola.
superDB exit 0 -------------------> Salir de la aplicaci�n.