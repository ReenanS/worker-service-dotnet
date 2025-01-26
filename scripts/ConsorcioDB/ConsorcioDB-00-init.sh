echo "Inicio Processamento"
echo "Aguardando instancia do SqlServer ativo ..."
sleep 120

/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P password@123456 -d master -i /tmp/ConsorcioDB-01-create-database.sql
/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P password@123456 -d master -i /tmp/ConsorcioDB-02-create-tables.sql
/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P password@123456 -d master -i /tmp/ConsorcioDB-03-insert-data.sql

echo "Fim processamento"