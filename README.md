# SharpSoSMS

SharpSoSMS é uma biblioteca .NET para acessar os serviços de envio de mensagens em massa do [SoSMS](http://sosms.com.br) da [tink!](http://tink.com.br).
O SoSMS permite que você mande mensagens SMS para vários destinatários ao mesmo tempo, permitindo você saber o status de cada mensagem enviada com detalhes.

## Utilização
Para usar a biblioteca você deverá construir o projeto (build) e colocar o arquivo *SharpSoSMS.dll* no diretário bin/ da sua aplicação.
Para configurar a biblioteca você precisará editar o arquivo de configuração da sua aplicação (.config) adicionando sua chave secreta para o SoSMS, conforme o exemplo abaixo:

	<appSettings>
		<add key="SoSMS.AuthToken" value="1a2b3c"/>
	</appSettings>

Para resgatar o seu código de segurança [acesse sua conta](http://sosms.com.br/usuarios/acessar) e verifique sua chave secreta na página da [documentação](http://sosms.com.br/pagina/documentacao#chave).

Para que você possa enviar suas mensagens é necessário que você confirme o número de celular informado no momento do cadastro. Para mais detalhes acesse a [documentação](http://sosms.com.br/pagina/documentacao#ativacao).

## Funcionalidades

### Enviando uma mensagem

O SoSMS permite o envio de uma mesma mensagem para vários destinatários ao mesmo tempo. Existem duas formas de enviar mensagens.

A primeira é passando os contatos em um array de strings conforme o exemplo abaixo:

	using SharpSoSMS;

	string[] contacts = {"Maria:1187965545", "Luana:8189965474", "Francisco:8388496535"};
	SoSMSMessage message = SoSMSClient.SendMessage("Bem Vindo!", contacts);

O primeiro parâmetro é o texto da mensagem a ser enviado. Este deve conter no máximo 140 caracteres. Já o segundo parâmetro é um array de strings contendo os dados dos destinatários.
Cada destinatário deve possuir um nome e um número de telefone que devem ser separados pelo caractere de dois pontos (:). 
Os números de telefone deverão possuir DDD e o número, totalizando 10 caracteres.

No exemplo acima será enviada uma mensagem com o texto "Bem Vindo!" para três destinatários:

 - Maria - (11) 8796-5545
 - Luana - (81) 8996-5474
 - Francisco - (83) 8879-6535

Este método retorna um objeto do tipo SoSMSMessage que possui as seguintes características:

    message.Id // O identificador da mensagem
	message.Text // A mensagem enviada aos destinatários
	message.Dispaches // Uma lista com os envios para cada destinatário, do tipo SoSMSMessageDispach

Cada dispach (SoSMSMessageDispach) possui as seguintes características:

    messageDispach.phoneNumber // O número do telefone do destinatário no formato "(99) 9999-9999"
	messageDispach.Status // O status da entrega da mensagem para o destinatário

Para mais informações sobre os possíveis status de retorno verifique a [documentação](http://sosms.com.br/pagina/documentacao#resposta).

O código abaixo mostra como você pode enviar uma mensagem para vários destinatários ao mesmo tempo passando a lista de destinatários como uma string única.

	using SharpSoSMS;

	SoSMSMessage message = SoSMSClient.SendMessage("Bem Vindo!", "Maria:1187965545,Luana:8189965474,Francisco:8388496535");

Neste, caso haja mais de um destinatário, os mesmos devem ser separados por vírgula (,).

### Resgatando o status de uma mensagem

	using SharpSoSMS;

	int id = 1002;
	SoSMSMessage message = SoSMSClient.GetMessage(id);

Este método retorna um objeto do tipo SoSMSMessage descrito acima.

### Saldo da conta

Para saber qual o saldo atual da sua conta você pode usar o seguinte código:

	using SharpSoSMS;

	SoSMSClient.GetBalance().Value;

Este código retorna um valor inteiro com o saldo atual da sua conta.

## TODO
Documentar no README as exceções

## Documentação oficial
Toda a documentação da API pode ser encontrada no site do [SoSMS](http://sosms.com.br/pagina/documentacao).

## Créditos
Parte da implementação desta biblioteca foi baseada no projeto [SharpBrake](https://github.com/asbjornu/SharpBrake) mantido por [Asbjørn Ulsberg](https://github.com/asbjornu).