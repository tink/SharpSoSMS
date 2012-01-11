# SharpSoSMS
SharpSoSMS � uma biblioteca .NET para acessar os servi�os de envio de mensagens em massa do [SoSMS](http://sosms.com.br) da [tink!](http://tink.com.br).
O SoSMS permite que voc� mande mensagens SMS para v�rios destinat�rios ao mesmo tempo, permitindo voc� saber o status de cada mensagem enviada com detalhes.

## Utiliza��o
Para usar a biblioteca voc� dever� construir o projeto (build) e colocar o arquivo *SharpSoSMS.dll* no diret�rio bin/ da sua aplica��o.
Para configurar a biblioteca voc� precisr� editar o arquivo de configura��o da sua aplica��o (.config) adicionando sua chave secreta para o SoSMS, conforme o exemplo abaixo:

    <appSettings>
		<add key="SoSMS.AuthToken" value="1a2b3c"/>
	</appSettings>

## Funcionalidades

### Saldo da conta

Para saber qual o saldo atual da sua conta voc� pode usar o seguinte c�digo:

	using SharpSoSMS;

	SoSMSClient.GetBalance().Value;

Este c�digo retornar um valor inteiro com o saldo atual da sua conta.

### Resgatando o status de uma mensagem

	using SharpSoSMS;

	int id = 1002;
	SoSMSMessage message = SoSMSCliente.GetMessage(id);

Este m�todo retorna um objeto do tipo SoSMSMessage que possui as seguintes caracter�sticas:

    message.Id // O identificador da mensagem
	message.Text // A mensagem enviada aos destinat�rios
	message.Dispaches // Uma lista com os envios para cada destinat�rio, do tipo SoSMSMessageDispach

Cada dispach (SoSMSMessageDispach) possui as seguintes caracter�sticas:

    messageDispach.phoneNumber // O n�mero do telefone do destinat�rio no formato "(99) 9999-9999"
	messageDispach.Status // O status da entrega da mensagem para o destinat�rio

Para mais informa��es sobre os poss�veis status de retorno verifique a [documenta��o](http://sosms.com.br/pagina/documentacao#resposta).

## TODO
Criar os m�todos de envio de mensagens

Criar testes unit�rios para fingir o acesso aos servi�os do SoSMS, usando Mock Responses.

## Documenta��o oficial
Toda a documenta��o da API pode ser encontrada no site do [SoSMS](http://sosms.com.br/pagina/documentacao).

## Cr�ditos
Parte da implementa��o desta biblioteca foi baseada no projeto [SharpBrake](https://github.com/asbjornu/SharpBrake) mantido por [Asbj�rn Ulsberg](https://github.com/asbjornu).