
if not exists(select 1 from syscolumns where id = object_id('menu'))
BEGIN
	CREATE TABLE [dbo].[menu](
		[id] [int] identity(1,1) NOT NULL,
		[menu] [varchar](30) NULL,
		[ordem] [int] NULL,
		[icone] [varchar](50) NULL,
	PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY]
	) ON [PRIMARY]
	

	SET IDENTITY_INSERT [dbo].[menu] ON 
	INSERT INTO menu (id, menu, ordem, icone) VALUES (1, 'Cadastros', 1, 'pe-7s-plus');
	INSERT INTO menu (id, menu, ordem, Icone) VALUES (6, 'Configurações', 6, 'pe-7s-config');
	SET IDENTITY_INSERT [dbo].[menu] OFF	
	
	ALTER TABLE [dbo].[menu] ADD  DEFAULT ((0)) FOR [ordem]
	ALTER TABLE [dbo].[menu] ADD  DEFAULT ('') FOR [icone]
END
GO

if not exists(select 1 from syscolumns where id = object_id('menu_opcoes'))
BEGIN
	CREATE TABLE [dbo].[menu_opcoes](
	[id] [int] identity(1,1) NOT NULL,
	[id_menu] [int] NULL,
	[path_url] [varchar](60) NOT NULL,
	[submenu] [int] NULL,
	[titulo] [varchar](50) NULL,
	[ativo] [bit] NULL,
	[visivel_menu] [bit] NULL,
	[slug_permissao] [varchar](50) NULL,
	 CONSTRAINT [_copy_2] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY]
	) ON [PRIMARY]

	SET IDENTITY_INSERT [dbo].[menu_opcoes] ON
	INSERT INTO menu_opcoes (id, id_menu, path_url, submenu, titulo, ativo, visivel_menu, slug_permissao) VALUES (3, 6, 'menuopcoes', 1, 'Menu de Opções', 1, 1, 'MENUOPCOES');
	INSERT INTO menu_opcoes (id, id_menu, path_url, submenu, titulo, ativo, visivel_menu, slug_permissao) VALUES (13, 1, 'usuarios', 1, 'Usuários', 1, 1, 'CADUSUARIOS');
	INSERT INTO menu_opcoes (id, id_menu, path_url, submenu, titulo, ativo, visivel_menu, slug_permissao) VALUES (14, 1, 'usuarios-perfil', 1, 'Usuários-Perfil', 1, 1, 'CADUSUPERFIL');
	SET IDENTITY_INSERT [dbo].[menu_opcoes] OFF

	ALTER TABLE [dbo].[menu_opcoes] ADD  DEFAULT ((0)) FOR [ativo]
	ALTER TABLE [dbo].[menu_opcoes] ADD  DEFAULT ((0)) FOR [visivel_menu]

	ALTER TABLE [dbo].[menu_opcoes]  WITH CHECK ADD FOREIGN KEY([id_menu])
	REFERENCES [dbo].[menu] ([id])

END
GO

if not exists(select 1 from syscolumns where id = object_id('menu_opcoes_botoes'))
BEGIN
	CREATE TABLE [dbo].[menu_opcoes_botoes](
	[id] [int] identity(1,1) NOT NULL,
	[id_permissoes] [int] NOT NULL,
	[id_menu_opcoes] [int] NOT NULL,
	 CONSTRAINT [_copy_1] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY]
	) ON [PRIMARY]

	SET IDENTITY_INSERT [dbo].[menu_opcoes_botoes] ON 	
	INSERT INTO menu_opcoes_botoes (id, id_permissoes, id_menu_opcoes) VALUES (39, 3, 13);
	INSERT INTO menu_opcoes_botoes (id, id_permissoes, id_menu_opcoes) VALUES (40, 3, 14);
	INSERT INTO menu_opcoes_botoes (id, id_permissoes, id_menu_opcoes) VALUES (41, 1, 14);
	INSERT INTO menu_opcoes_botoes (id, id_permissoes, id_menu_opcoes) VALUES (42, 2, 14);
	INSERT INTO menu_opcoes_botoes (id, id_permissoes, id_menu_opcoes) VALUES (43, 1, 13);
	INSERT INTO menu_opcoes_botoes (id, id_permissoes, id_menu_opcoes) VALUES (44, 2, 13);	
	SET IDENTITY_INSERT [dbo].[menu_opcoes_botoes] OFF

	ALTER TABLE [dbo].[menu_opcoes_botoes]  WITH CHECK ADD  CONSTRAINT [fk_menu_opcoes_botoes_menu_opcoes] FOREIGN KEY([id_menu_opcoes])
	REFERENCES [dbo].[menu_opcoes] ([id])
	ALTER TABLE [dbo].[menu_opcoes_botoes] CHECK CONSTRAINT [fk_menu_opcoes_botoes_menu_opcoes]

END
GO

if not exists(select 1 from syscolumns where id = object_id('modulo'))
BEGIN
	CREATE TABLE [dbo].[modulo](
	[id] [int] identity(1,1) NOT NULL,
	[nome] [varchar](30) NULL,
	 CONSTRAINT [_copy_6] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY]
	) ON [PRIMARY]

	SET IDENTITY_INSERT [dbo].[modulo] ON 
	INSERT INTO modulo (id, nome) VALUES (1, 'Varejo');
	INSERT INTO modulo (id, nome) VALUES (2, 'Supermercado');
	INSERT INTO modulo (id, nome) VALUES (3, 'Sapataria');
	INSERT INTO modulo (id, nome) VALUES (4, 'Farmácia');
	INSERT INTO modulo (id, nome) VALUES (5, 'Autopeças');
	SET IDENTITY_INSERT [dbo].[modulo] OFF
	END
GO

if not exists(select 1 from syscolumns where id = object_id('modulo_menu_opcoes'))
BEGIN
	CREATE TABLE [dbo].[modulo_menu_opcoes](
	[id] [int] identity(1,1) NOT NULL,
	[id_modulo] [int] NOT NULL,
	[id_menu_opcoes] [int] NOT NULL,
	 CONSTRAINT [_copy_7] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY]
	) ON [PRIMARY]

	SET IDENTITY_INSERT [dbo].[modulo_menu_opcoes] ON 
	INSERT INTO modulo_menu_opcoes (id, id_modulo, id_menu_opcoes) VALUES (5, 1, 3);	
	INSERT INTO modulo_menu_opcoes (id, id_modulo, id_menu_opcoes) VALUES (16, 1, 13);
	INSERT INTO modulo_menu_opcoes (id, id_modulo, id_menu_opcoes) VALUES (17, 1, 14);		
	SET IDENTITY_INSERT [dbo].[modulo_menu_opcoes] OFF

	ALTER TABLE [dbo].[modulo_menu_opcoes]  WITH CHECK ADD  CONSTRAINT [fk_modulo_menu_opcoes_menu_opcoes] FOREIGN KEY([id_menu_opcoes])
	REFERENCES [dbo].[menu_opcoes] ([id])
	ALTER TABLE [dbo].[modulo_menu_opcoes] CHECK CONSTRAINT [fk_modulo_menu_opcoes_menu_opcoes]

	ALTER TABLE [dbo].[modulo_menu_opcoes]  WITH CHECK ADD  CONSTRAINT [fk_modulo_menu_opcoes_modulo] FOREIGN KEY([id_modulo])
	REFERENCES [dbo].[modulo] ([id])

	ALTER TABLE [dbo].[modulo_menu_opcoes] CHECK CONSTRAINT [fk_modulo_menu_opcoes_modulo]

END
GO

if not exists(select 1 from syscolumns where id = object_id('perfil'))
BEGIN
	CREATE TABLE [dbo].[perfil](
	[id] [int] identity(1,1) NOT NULL,
	[nome] [varchar](20) NOT NULL,
	 CONSTRAINT [_copy_5] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	SET IDENTITY_INSERT [dbo].[perfil] ON 
	INSERT INTO perfil (id, nome) VALUES (1, 'Administrativo');
	SET IDENTITY_INSERT [dbo].[perfil] OFF
END
GO

if not exists(select 1 from syscolumns where id = object_id('perfil_usuario'))
BEGIN
	CREATE TABLE [dbo].[perfil_usuario](
	[id] [int] identity(1,1) NOT NULL,
	[id_modulo_menu_opc] [int] NOT NULL,
	[id_perfil] [int] NOT NULL,
	 CONSTRAINT [_copy_4] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	SET IDENTITY_INSERT [dbo].[perfil_usuario] ON 	
	INSERT INTO perfil_usuario (id, id_modulo_menu_opc, id_perfil) VALUES (14, 5, 1);	
	INSERT INTO perfil_usuario (id, id_modulo_menu_opc, id_perfil) VALUES (16, 17, 1);	
	INSERT INTO perfil_usuario (id, id_modulo_menu_opc, id_perfil) VALUES (26, 16, 1);
	SET IDENTITY_INSERT [dbo].[perfil_usuario] OFF

	ALTER TABLE [dbo].[perfil_usuario]  WITH CHECK ADD  CONSTRAINT [fk_perfil_usuario_modulo_menu_opcoes] FOREIGN KEY([id_modulo_menu_opc])
	REFERENCES [dbo].[modulo_menu_opcoes] ([id])

	ALTER TABLE [dbo].[perfil_usuario] CHECK CONSTRAINT [fk_perfil_usuario_modulo_menu_opcoes]

	ALTER TABLE [dbo].[perfil_usuario]  WITH CHECK ADD  CONSTRAINT [fk_perfil_usuario_perfil] FOREIGN KEY([id_perfil])
	REFERENCES [dbo].[perfil] ([id])

	ALTER TABLE [dbo].[perfil_usuario] CHECK CONSTRAINT [fk_perfil_usuario_perfil]
END
GO

if not exists(select 1 from syscolumns where id = object_id('perfil_usuario_botoes'))
BEGIN
	CREATE TABLE [dbo].[perfil_usuario_botoes](
	[id] [int] identity(1,1) NOT NULL,
	[id_permissoes] [int] NOT NULL,
	[id_perfil_usuario] [int] NULL,
	 CONSTRAINT [_copy_3] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	SET IDENTITY_INSERT [dbo].[perfil_usuario_botoes] ON 	
	INSERT INTO perfil_usuario_botoes (id, id_permissoes, id_perfil_usuario) VALUES (19, 1, 14);
	INSERT INTO perfil_usuario_botoes (id, id_permissoes, id_perfil_usuario) VALUES (20, 2, 14);
	INSERT INTO perfil_usuario_botoes (id, id_permissoes, id_perfil_usuario) VALUES (21, 3, 14);
	INSERT INTO perfil_usuario_botoes (id, id_permissoes, id_perfil_usuario) VALUES (60, 1, 16);
	INSERT INTO perfil_usuario_botoes (id, id_permissoes, id_perfil_usuario) VALUES (61, 2, 16);
	INSERT INTO perfil_usuario_botoes (id, id_permissoes, id_perfil_usuario) VALUES (64, 3, 16);
	INSERT INTO perfil_usuario_botoes (id, id_permissoes, id_perfil_usuario) VALUES (66, 3, 26);
	INSERT INTO perfil_usuario_botoes (id, id_permissoes, id_perfil_usuario) VALUES (67, 1, 26);
	INSERT INTO perfil_usuario_botoes (id, id_permissoes, id_perfil_usuario) VALUES (68, 2, 26);
	SET IDENTITY_INSERT [dbo].[perfil_usuario_botoes] OFF

	ALTER TABLE [dbo].[perfil_usuario_botoes]  WITH CHECK ADD  CONSTRAINT [fk_perfil_usuario_botoes_id_perfil_usuario] FOREIGN KEY([id_perfil_usuario])
	REFERENCES [dbo].[perfil_usuario] ([id])
	
	ALTER TABLE [dbo].[perfil_usuario_botoes] CHECK CONSTRAINT [fk_perfil_usuario_botoes_id_perfil_usuario]
END
GO

if not exists(select 1 from syscolumns where id = object_id('permissoes'))
BEGIN
	CREATE TABLE [dbo].[permissoes](
	[id] [int] identity(1,1) NOT NULL,
	[nome] [varchar](30) NOT NULL,
	[type_campo] [varchar](10) NOT NULL,
	 CONSTRAINT [_copy_8] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY]
	) ON [PRIMARY]

	SET IDENTITY_INSERT [dbo].[permissoes] ON 
	INSERT INTO permissoes (id, nome, type_campo) VALUES (1, 'Adicionar', 'butto');
	INSERT INTO permissoes (id, nome, type_campo) VALUES (2, 'Editar', 'butto');
	INSERT INTO permissoes (id, nome, type_campo) VALUES (3, 'Excluir', 'butto');
	SET IDENTITY_INSERT [dbo].[permissoes] OFF

	ALTER TABLE [dbo].[perfil_usuario_botoes]  WITH CHECK ADD  CONSTRAINT [fk_perfil_usuario_botoes_id_permissoes] FOREIGN KEY([id_permissoes])
	REFERENCES [dbo].[permissoes] ([id])


	ALTER TABLE [dbo].[perfil_usuario_botoes] CHECK CONSTRAINT [fk_perfil_usuario_botoes_id_permissoes]

	/*ALTER TABLE [dbo].[PERFIL_USUARIO_BOTOES]  WITH CHECK ADD FOREIGN KEY([Id_Permissoes])
	REFERENCES [dbo].[PERMISSOES] ([Id])*/
END
GO

if not exists(select 1 from SYS.views where name = 'C_Perfil_Usuario')
BEGIN
	EXEC('CREATE VIEW [dbo].[c_perfil_usuario] AS select pub.Id, mo.Titulo as Menu, (upper(isnull(mo.slug_permissao + ''_'' + p.Nome, ''''))) as Permissao, pu.ID_Perfil as IdPerfil  
	from PERFIL_USUARIO as pu 
	inner join MODULO_MENU_OPCOES as mmo on pu.Id_MODULO_MENU_OPC = mmo.Id 
	inner join MENU_OPCOES as mo on mo.Id = mmo.Id_Menu_Opcoes  
	inner join PERFIL_USUARIO_BOTOES as pub on pub.Id_Perfil_Usuario = pu.Id 
	inner join PERMISSOES as p on p.Id = pub.Id_Permissoes where mo.Ativo = 1')
END
GO
