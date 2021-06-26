TUTORIAL DESAFIO TOTVS BACKEND


BANCO DE DADOS POSTGRES:

Foi utilizado uma imagem POSTGRES embarcada no DOCKER.
> docker pull postgres

Os testes foram realizados com a configuração padrão na porta 5432 com usuário postgres e uma senha para testes ‘1234’;

O seguinte comando foi usado para subir o postgres após o download da imagem:

> docker run -p 5432:5432 -v c:\pgdata:/var/lib/postgresql/data -e POSTGRES_PASSWORD=1234 -d postgres

Observe que foi criada uma pasta c:\pgdata para armazenar os dados do banco para evitar que eles fossem perdidos após o desligamento do DOCKER.

O script para a criação do banco de dados se encontra neste distribuição na pasta: /scriptdatabases/r3ctdata.sql


BACKEND:

Para o desenvolvimento do BACKEND foi utilizado o VISUAL STUDIO 2019. O projeto foi nomeado com o nome R3CTAPP. Segue uma lista com os ENDPOINTS expostos pela API:

GET		api/hirecandidates 			LISTA CANDIDATOS
GET		api/hirecandidates/news			LISTA CANDIDATOS SEM CANDIDATURAS
GET		api/hirecandidates/{id}			RETORNA 1 CANDIDATO
PUT		api/hirecandidates/{id}			ALTERA 1 CANDIDATO
POST		api/hirecandidates			INCLUIR 1 CANDIDATO
DELETE	api/hirecandidates/{id}			APAGA 1 CANDIDATO
GET		api/hirecandidates/{:id}/hirecurriculum	RETORNA 1 CURRICULO

GET		api/hirejobs				LISTA VAGAS
GET		api/hirejobs/{id}				RETORNA 1 VAGA
PUT		api/hirejobs/{id}		‘		ALTERA 1 VAGA
POST		api/hirejobs				INCLUIR 1 VAGA
DELETE	api/hirejobs/{id}				APAGA 1 VAGA
GET		api/hirejobs/{:id}/hireprocess		LISTA PROCESSOS

GET		api/hirecurriculums					
GET		api/hirecurriculums/{:id}			RETORNA 1 CURRICULO
PUT		api/hirecurriculums/{:id}			ALTERA 1 CURRICULO
POST		api/hirecurriculums			INCLUIR 1 CURRICULO
DELETE	api/hirecurriculums/{:id}			APAGA 1 CURRICULO

GET		api/hireprocess/{:id}			RETORNA 1 CANDIDATURA
PUT		api/hireprocess/{:id}			ALTERA 1 CANDIDATURA
POST		api/hireprocess				INSERE 1 CANDIDATURA
DELETE	api/hireprocess/{:id}			APAGA 1 CANDIDATURA


TESTES DE UNIDADES:

Foi implementado 21 testes em todos os ENDPOINTS para os retornos esperados.


TESTE AUTOMATIZADOS COM O POSTMAN

Foi criada uma coleção com vários testes que estão disponíveis para consulta.

A coleção foi exportada para um arquivo com o nome: ./testscollection/R3ctTests.postman_collection e se encontrado junto com a origem após o download ou a clonagem deste repositório.

A aplicação quando executada no ambiente local expõe o endereço https://localhost:44391 para usado.