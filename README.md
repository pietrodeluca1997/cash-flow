## Fluxo de Caixa - [Pietro Romano]

## Descrição:
Uma aplicação responsável pelo gerenciamento de fluxo de caixa, utilizando boas práticas de arquitetura de software, além da implementação do **Design Pattern ["Mediator"**] de uma forma **totalmente customizada!**


Foi escolhida a **arquitetura de microserviços** pela finalidade do projeto e simulação de ambientes que necessitam da utilização de aplicações distribuídas.

Todas as aplicações foram desenvolvidas em **.NET Core 6.0** e rodam dentro de **containers Docker** junto com todos os serviços depentes.

## Arquitetura da Solução
</br>
<img src="https://github.com/pietrodeluca1997/cash-flow/blob/master/documents/Cash%20Flow%20Solution%20Architecture.drawio.png?raw=true" />
</br>

## Aplicações:
* API de Identidade - Autenticação e gerenciamento de usuários;
* API de Contas - Gerenciamento de contas e de gerentes de conta;
* API de Transações - Efetuar movimentações financeiras em uma conta;
* API de Relatórios - Disponibilização de relatórios;
* API de Encaminhamento de Tráfego - Responsável pelo recebimento de todas as requisições e pelo encaminhamento aos serviços responsáveis;


## Serviços:
* [RabbitMQ](https://www.rabbitmq.com/) - Comunicação assíncrona entre as aplicações;
* [MongoDB](https://www.mongodb.com/) - Extrato (Relatório de transações sincronizados com a base relacional através do padrão CQRS);
* [PostgreSQL](https://www.postgresql.org/) - Usuários / Transações / Contas / Gerentes de Conta;
* [Redis](https://redis.com/) - Cache distribuído em motivo da concorrência de dados entre transações e conta em caso de mais do que uma máquina executando o projeto;


## Bibliotecas:
* [Ocelot](https://ocelot.readthedocs.io/en/latest/introduction/gettingstarted.html) - Encaminhamento de tráfego externo para os serviços apropriados internos;
* [EntityFramework](https://learn.microsoft.com/en-us/ef/) - ORM para comunicação com o PostgreSQL;
* [MassTransit](https://masstransit-project.com/) - Camada de abstração para gerenciamento de mensagerias;

## Como executar o projeto:

## Pré requisitos: 
* [Docker](https://www.docker.com/) 
* [Visual Studio](https://visualstudio.microsoft.com/pt-br/vs/) ou [VS Code](https://code.visualstudio.com/) com o [SDK do .NET Core 6.0]   (https://dotnet.microsoft.com/en-us/download/dotnet/6.0) ou superior.

## Etapa 1 -: Docker-Compose
* Va até o diretório "deployment/docker" na raíz do projeto e execute 
```powershell
docker-compose -f docker-compose.yml up
```

## Etapa 2 -: Migrations
Certifique-se que ao rodar a Migration através do Visual Studio, o projeto em questão esteja selecionado na janela do console do gerenciador de pacotes e como projeto padrão na área de execução.
* Execute os seguintes comandos na janela do console do gerenciador:

* Migration da API de Identidade
```powershell
Update-Database -StartUpProject CF.Identity.API
```

* Migration da API de Contas
```powershell
Update-Database -StartUpProject CF.Account.API
```


## Etapa 3 -: Consumir as APIs via postman
Segue link para a importação da collection do Postman para facilitar a utilização do projeto: (https://www.getpostman.com/collections/0ecd85eef71e66b6d9fe).
