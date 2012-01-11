# SharpSoSMS
SharpSoSMS é uma biblioteca .NET para acessar os serviços de envio de mensagens em massa do [SoSMS](http://sosms.com.br) da [tink!](http://tink.com.br).
O SoSMS permite que você mande mensagens SMS para vários destinatários ao mesmo tempo, permitindo você saber o status de cada mensagem enviada com detalhes.

## Utilização
Para usar a biblioteca você deverá construir o projeto (build) e colocar o arquivo *SharpSoSMS.dll* no diretário bin/ da sua aplicação.
Para configurar a biblioteca você precisará editar o arquivo de configuração da sua aplicação (.config) adicionando sua chave secreta para o SoSMS, conforme o exemplo abaixo:

    <appSettings>
		<add key="SoSMS.AuthToken" value="1a2b3c"/>
	</appSettings>

## Funcionalidades

### Saldo da conta

Para saber qual o saldo atual da sua conta você pode usar o seguinte código:

	using SharpSoSMS;

	SoSMSClient.GetBalance().Value;

Este código retornar um valor inteiro com o saldo atual da sua conta.

### Resgatando o status de uma mensagem

	using SharpSoSMS;

	int id = 1002;
	SoSMSMessage message = SoSMSCliente.GetMessage(id);

Este método retorna um objeto do tipo SoSMSMessage que possui as seguintes características:

    message.Id // O identificador da mensagem
	message.Text // A mensagem enviada aos destinatários
	message.Dispaches // Uma lista com os envios para cada destinatário, do tipo SoSMSMessageDispach

Cada dispach (SoSMSMessageDispach) possui as seguintes características:

    messageDispach.phoneNumber // O número do telefone do destinatário no formato "(99) 9999-9999"
	messageDispach.Status // O status da entrega da mensagem para o destinatário

Para mais informações sobre os possíveis status de retorno verifique a [documentação](http://sosms.com.br/pagina/documentacao#resposta).

## TODO
Criar os métodos de envio de mensagens

Criar testes unitários para fingir o acesso aos serviços do SoSMS, usando Mock Responses.

## Documentação oficial
Toda a documentação da API pode ser encontrada no site do [SoSMS](http://sosms.com.br/pagina/documentacao).

## Créditos
Parte da implementação desta biblioteca foi baseada no projeto [SharpBrake](https://github.com/asbjornu/SharpBrake) mantido por [Asbjørn Ulsberg](https://github.com/asbjornu).