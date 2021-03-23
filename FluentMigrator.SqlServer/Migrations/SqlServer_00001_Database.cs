namespace FluentMigrator.SqlServer.Migrations
{
    [FluentMigrator.Migration(00001)]
    public class SqlServer_00001_Database: FluentMigrator.Migration
    {
        public override void Down()
        {

        }

        public override void Up()
        {
            Execute.Sql(
			@" if not exists(select 1 from syscolumns where id = object_id('filme'))  
			BEGIN 
				create table filme( 
					Id int primary key not null, 
					Nome varchar(100) not null, 
					Resumo varchar(100) not null, 
					Tempo decimal(10, 2) not null, 
					Ano int not null, 
					foto varchar(200) not null, 
				); 
			END; 

			if not exists(select 1 from syscolumns where id = object_id('Ator')) 
			BEGIN 
				create table Ator( 
					Id int primary key not null, 
					Nome varchar(100) not null 
				); 
			END; 

			if not exists(select 1 from syscolumns where id = object_id('Escritor')) 
			BEGIN 
				create table Escritor( 
					Id int primary key not null, 
					Nome varchar(100) not null 
				); 
			END; 

			if not exists(select 1 from syscolumns where id = object_id('Genero')) 
			BEGIN 
				create table Genero( 
					Id int primary key not null, 
					Nome varchar(100) not null 
				); 
			END; 

			if not exists(select 1 from syscolumns where id = object_id('Diretor')) 
			BEGIN 
				create table Diretor( 
					Id int primary key not null, 
					Nome varchar(100) not null 
				);  
			END;  


			if not exists(select 1 from syscolumns where id = object_id('FilmeXAtor'))  
			BEGIN  
			create table FilmeXAtor(  
				Id int primary key not null,  
				IdFilme int not null,  
				IdAtor int not null,  
				CONSTRAINT FK_FilmeA FOREIGN KEY(IdFilme) REFERENCES Filme(Id),  
				CONSTRAINT FK_AtorF FOREIGN KEY(IdAtor)REFERENCES Ator(Id)  
			);  
			END;  

			if not exists(select 1 from syscolumns where id = object_id('FilmeXEscritor'))  
			BEGIN  
				create table FilmeXEscritor(  
					Id int primary key not null,  
					IdFilme int not null, 
					IdEscritor int not null, 
					CONSTRAINT FK_FilmeE FOREIGN KEY(IdFilme) REFERENCES Filme(Id), 
					CONSTRAINT FK_Esc FOREIGN KEY(IdEscritor)REFERENCES Escritor(Id) 
				); 
			END; 
 
			if not exists(select 1 from syscolumns where id = object_id('FilmeXGenero')) 
			BEGIN 
				create table FilmeXGenero( 
					Id int primary key not null, 
					IdFilme int not null, 
					IdGenero int not null, 
					CONSTRAINT FK_FilmeG FOREIGN KEY(IdFilme) REFERENCES Filme(Id), 
					CONSTRAINT FK_Gen FOREIGN KEY(IdGenero)REFERENCES Genero(Id) 
				); 
			END; 

			if not exists(select 1 from syscolumns where id = object_id('FilmeXDiretor')) 
			BEGIN 
				create table FilmeXDiretor( 
					Id int primary key not null, 
					IdFilme int not null,  
					IdDiretor int not null, 
					CONSTRAINT FK_FilmeD FOREIGN KEY(IdFilme) REFERENCES Filme(Id), 
					CONSTRAINT FK_Diretor FOREIGN KEY(IdDiretor)REFERENCES Diretor(Id) 
				); 
			END; 

			if not exists(select 1 from syscolumns where id = object_id('FilmeXNota')) 
			BEGIN 
				create table FilmeXNota( 
					Id int primary key not null, 
					IdUsuario nvarchar(450) not null, 
					IdFilme int not null, 
					Nota int not null, 
					CONSTRAINT FK_FilmeN FOREIGN KEY(IdFilme) REFERENCES Filme(Id), 
					CONSTRAINT FK_Nota FOREIGN KEY(IdUsuario)REFERENCES Usuarios(Id) 
				);
			END; ");
        }
    }
}
