# Ejemplo del Patr�n Observer - Sistema Integrado

Este ejemplo demuestra c�mo funciona el patr�n Observer en el Sistema Integrado de Alertas M�dicas, probando directamente los observadores existentes en el sistema.

## C�mo ejecutar el ejemplo

1. Abrir la soluci�n en Visual Studio
2. Establecer el proyecto "SistemaIntegradoAlertas" como proyecto de inicio
3. Ejecutar el programa (F5)

## Estructura del patr�n Observer implementado

El programa prueba directamente los observadores existentes en el sistema:

### 1. Observer para Creaci�n de Alertas

- **AlertaCreacionObserver**: Implementaci�n que notifica cuando se crea una nueva alerta m�dica.
  - M�todo: `OnElementoCreado(Alerta elemento)`
  - Retorna un mensaje descriptivo de la alerta creada

### 2. Observer para Comunicaciones

- **LogComunicacionObserver**: Implementaci�n que registra las comunicaciones en logs.
  - M�todo: `OnNotificacionEnviada(string mensaje, Perfil destinatario)`
  - Retorna un mensaje de log con la informaci�n de la comunicaci�n

## Caracter�sticas del patr�n Observer

El patr�n Observer permite que un objeto (sujeto) mantenga una lista de sus dependientes (observadores) y les notifique autom�ticamente cuando cambia su estado. En este sistema:

1. **Creaci�n de Alertas**: Cuando se crea una nueva alerta m�dica, los observadores registrados son notificados para realizar acciones como:
   - Registrar la alerta en logs
   - Enviar notificaciones a personal m�dico
   - Actualizar estad�sticas del sistema

2. **Comunicaciones**: Cuando se env�a un mensaje, los observadores son notificados para:
   - Registrar la comunicaci�n en logs de auditor�a
   - Detectar mensajes urgentes y elevar su prioridad
   - Mantener un historial de comunicaciones

## Aplicaci�n en el Sistema M�dico

Este patr�n es crucial en un sistema m�dico donde:

- La creaci�n de alertas debe notificar a m�ltiples sistemas (logs, notificaciones, estad�sticas).
- Las comunicaciones deben ser monitoreadas para fines de auditor�a, emergencia y cumplimiento normativo.
- Se necesita reaccionar en tiempo real a eventos importantes.

En este ejemplo, se puede observar el comportamiento de los observadores al probar directamente sus m�todos con diferentes entradas, como alertas de distintos niveles de criticidad y mensajes con diferentes destinatarios y contenidos.