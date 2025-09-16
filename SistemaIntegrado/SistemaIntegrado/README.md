# Ejemplo del Patrón Observer - Sistema Integrado

Este ejemplo demuestra cómo funciona el patrón Observer en el Sistema Integrado de Alertas Médicas, probando directamente los observadores existentes en el sistema.

## Cómo ejecutar el ejemplo

1. Abrir la solución en Visual Studio
2. Establecer el proyecto "SistemaIntegradoAlertas" como proyecto de inicio
3. Ejecutar el programa (F5)

## Estructura del patrón Observer implementado

El programa prueba directamente los observadores existentes en el sistema:

### 1. Observer para Creación de Alertas

- **AlertaCreacionObserver**: Implementación que notifica cuando se crea una nueva alerta médica.
  - Método: `OnElementoCreado(Alerta elemento)`
  - Retorna un mensaje descriptivo de la alerta creada

### 2. Observer para Comunicaciones

- **LogComunicacionObserver**: Implementación que registra las comunicaciones en logs.
  - Método: `OnNotificacionEnviada(string mensaje, Perfil destinatario)`
  - Retorna un mensaje de log con la información de la comunicación

## Características del patrón Observer

El patrón Observer permite que un objeto (sujeto) mantenga una lista de sus dependientes (observadores) y les notifique automáticamente cuando cambia su estado. En este sistema:

1. **Creación de Alertas**: Cuando se crea una nueva alerta médica, los observadores registrados son notificados para realizar acciones como:
   - Registrar la alerta en logs
   - Enviar notificaciones a personal médico
   - Actualizar estadísticas del sistema

2. **Comunicaciones**: Cuando se envía un mensaje, los observadores son notificados para:
   - Registrar la comunicación en logs de auditoría
   - Detectar mensajes urgentes y elevar su prioridad
   - Mantener un historial de comunicaciones

## Aplicación en el Sistema Médico

Este patrón es crucial en un sistema médico donde:

- La creación de alertas debe notificar a múltiples sistemas (logs, notificaciones, estadísticas).
- Las comunicaciones deben ser monitoreadas para fines de auditoría, emergencia y cumplimiento normativo.
- Se necesita reaccionar en tiempo real a eventos importantes.

En este ejemplo, se puede observar el comportamiento de los observadores al probar directamente sus métodos con diferentes entradas, como alertas de distintos niveles de criticidad y mensajes con diferentes destinatarios y contenidos.