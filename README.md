# StandVirtual

Bem-vindo ao repositório do projeto **StandVirtual**. Este tutorial passo a passo irá guiá-lo na configuração e execução do projeto. O StandVirtual é uma aplicação desenvolvida em **ASP.NET MVC 5** com integração a uma **Base de Dados SQL Server**, que permite a gestão de stands automóveis.

---

## Estrutura do Tutorial
1. **Base de Dados**: Como restaurar a base de dados a partir de um ficheiro de backup.
2. **Configuração do Visual Studio**: Pacotes necessários para editar e compilar o projeto.
3. **Clonar o Repositório**: Como obter o projeto localmente.
4. **Executar o Projeto**: Corrigir erros e executar a aplicação.
5. **Credenciais de Administrador**: Como aceder ao sistema como administrador.

---

## 1. Base de Dados
Aceder ao ficheiro do [Backup](https://github.com/GoncaloSantos2005/GestaoStandVirtual_AAD/blob/master/BackupDB/StandVirtualTestes_LogBackup_2024-12-31_17-20-09.bak) e fazer o download. 

> **Nota**: Adicione o ficheiro à pasta de backups padrão do SQL Server Management Studio (normalmente localizada em `C:\Program Files\Microsoft SQL Server\MSSQLXX.SQLEXPRESS\MSSQL\Backup`).

Depois, abra o Microsoft SQL Server Management Studio e siga os passos:

1. Fazer login no servidor.
2. Clicar com o botão direito em **Databases**.
3. Selecionar **Restore Database**.
4. Em *Source*, selecionar **Device** e clicar nos '...'.
5. Clicar em **Add** e selecionar o ficheiro de backup transferido anteriormente.
6. Depois de adicionar, clicar em **OK** e novamente em **OK**.
7. Na aba esquerda, clicar em **Options**.
8. Selecionar a opção **Overwrite the existing database**.
9. Clicar em **OK**.

Após concluir o processo de restauração, verifique se todas as tabelas estão presentes e se a base de dados foi criada com o nome *StandVirtualTestes*.

---

## 2. Configuração Visual Studio (Opcional)
Como o projeto foi desenvolvido em **ASP.NET MVC 5**, pode ser necessário instalar alguns pacotes caso pretenda editar o código.

### Cargas de Trabalho:
1. ASP.NET e Desenvolvimento Web.
2. Desenvolvimento para Desktop com .NET.

### Componentes Individuais:
1. SDK do .NET Framework 4.8.
2. Pacote de direcionamento do .NET Framework.
3. Compiladores C# e Visual Basic Roslyn.
4. MSBuild.
5. C# e Visual Basic.
6. Gestor de Pacotes NuGet.
7. Driver ODBC do SQL Server.
8. Tipos de Dados CLR para SQL Server.
9. OpenJDK.
10. Runtime do .NET.

>[!Note]
> Estes pacotes não são obrigatórios para executar o projeto, mas podem ser úteis para o desenvolvimento.

---

## 3. Clonar o Repositório
1. Abrir o Visual Studio.
2. Clicar em **Clone a repository**.
3. Em *Repository location*, colocar o link: [Repositório StandVirtual](https://github.com/GoncaloSantos2005/GestaoStandVirtual_AAD/tree/master).
4. Selecionar o **Path** onde ficará o projeto.
5. Clicar em **Clone**.

Após clonar o projeto:

1. Abrir o **Solution Explorer**.
2. Na pasta *Models*, apagar o ficheiro **Model1.edmx**.
3. Clicar com o botão direito em *Models*, selecionar **Add** e depois **New Item**.
4. Clicar em **Show All Templates** (se disponível).
5. No grupo *C#*, selecionar *Data* e clicar duas vezes em **ADO.NET Entity Data Model**.
6. Selecionar **EF Designer from database**.
7. Clicar em **New Connection**.
8. Em **Server name**, colocar o nome do servidor onde está alocada a base de dados.

> **Nota**: Caso sejam necessárias credenciais para entrar no servidor, selecione *SQL Server Authentication* e insira as credenciais correspondentes.

9. Selecionar **Trust Server Certificate**.
10. Selecionar a base de dados *StandVirtualTestes* em **Select or enter a database name**.
11. Clicar em **OK** e depois em **Next>**.
12. Selecionar **Tables** e **Stored Procedures and Functions**.
13. Clicar em **Finish**.

---

## 4. Executar o Projeto
1. Com o projeto aberto, clicar em **IIS Express** ou pressionar **F5**.

Se aparecer o aviso *Microsoft Visual Studio* "There were build errors...", clique em **No**.
Na *Error List*, irão surgir erros. Para corrigir, substitua:

`StandVirtualTestesEntities1` por **StandVirtualTestesEntities2**.

Depois de corrigir os erros, faça o seguinte:

1. Aceda à [pasta Bin](https://github.com/GoncaloSantos2005/GestaoStandVirtual_AAD/tree/master/Bin) no repositório e faça o download dos ficheiros necessários.
2. Coloque-os na pasta *Bin* do projeto local (por exemplo: `C:\Users\Utilizador\source\repos\StandVirtual\bin`).

Finalmente, clique novamente em **IIS Express** ou pressione **F5** para executar o projeto.

---

## 5. Credenciais de Administrador
Para aceder ao sistema como administrador, utilize as seguintes credenciais:

- **Email**: goncalosantos513@gmail.com
- **Password**: 1234
