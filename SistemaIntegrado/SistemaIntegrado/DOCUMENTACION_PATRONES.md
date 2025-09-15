# DOCUMENTACION DE PATRONES DE DISE�O - SISTEMA INTEGRADO DE ALERTAS MEDICAS

## RESUMEN GENERAL

**Sistema**: Sistema Integrado de Alertas M�dicas
**Tecnolog�a**: .NET 8, C# 12.0
**Total de Patrones Implementados**: 5 patrones GoF
**Arquitectura**: Sistema m�dico con funcionalidades CRUD completas

---

## 1. PATRON SINGLETON

### Ubicaci�n
- **Funcionalidad**: `Clases/Singleton/`

### Componentes

#### Clases Principales
- **SistemaIntegrado.cs**
  - Constructor privado
  - Instancia est�tica privada
  - M�todo p�blico est�tico:
    - `GetInstance()` - Obtiene la �nica instancia del sistema
  - Propiedades p�blicas:
    - `Nombre { get; set; }` - Nombre del sistema
    - `Telefono { get; set; }` - Tel�fono del sistema
    - `L_cuentas { get; set; }` - Lista de cuentas registradas
    - `L_centroMedico { get; set; }` - Lista de centros m�dicos

### Prop�sito
Garantizar una sola instancia del sistema integrado de alertas m�dicas en toda la aplicaci�n.

---

## 2. PATRON FACTORY METHOD

### Ubicaci�n
- **Funcionalidad**: `Funcionalidad/Crear/Factory/`

### Componentes

#### Interfaces
- **IPerfilFactory.cs**
  - `CrearPerfil(string nombre, string correo, string tipoCedula, uint cedula, uint celular, string contrasena, params object[] extras)` - M�todo de creaci�n gen�rico

#### Clases Implementadoras
- **AdminFactory.cs**
  - `CrearPerfil(...)` - Crea perfiles de tipo Administrador

- **OperadorFactory.cs**
  - `CrearPerfil(...)` - Crea perfiles de tipo Operador

- **ParamedicoFactory.cs**
  - `CrearPerfil(...)` - Crea perfiles de tipo Param�dico

- **PacienteFactory.cs**
  - `CrearPerfil(...)` - Crea perfiles de tipo Paciente

### Inyecciones
- Se inyectan en servicios de creaci�n seg�n el tipo de perfil requerido

### Prop�sito
Crear diferentes tipos de perfiles m�dicos (Admin, Operador, Param�dico, Paciente) de forma estandarizada y desacoplada.

---

## 3. PATRON OBSERVER (Doble Implementaci�n)

### 3.1 Observer para Creaci�n de Alertas

#### Ubicaci�n
- **Funcionalidad**: `Funcionalidad/Crear/Observer/`

#### Componentes

##### Interfaces
- **ICreacionObserver<T>.cs**
  - `OnElementoCreado(T elemento)` - Se ejecuta cuando se crea un nuevo elemento

##### Clases
- **AlertaCreacionObserver.cs**
  - `OnElementoCreado(Alerta elemento)` - Maneja la notificaci�n cuando se crea una nueva alerta m�dica

### 3.2 Observer para Comunicaciones

#### Ubicaci�n
- **Funcionalidad**: `Funcionalidad/Comunicacion/Observer/`

#### Componentes

##### Interfaces
- **INotificacionObserver.cs**
  - `OnNotificacionEnviada(string mensaje, Perfil destinatario)` - Se ejecuta cuando se env�a una notificaci�n

##### Clases
- **LogComunicacionObserver.cs**
  - `OnNotificacionEnviada(string mensaje, Perfil destinatario)` - Registra las comunicaciones en logs

##### Inyecciones
- **ComunicacionOperador.cs**
  - Propiedades:
    - `Comunicacion { get; }` - Interfaz de comunicaci�n para operadores

- **ComunicacionParamedico.cs**
  - Propiedades:
    - `Comunicacion { get; }` - Interfaz de comunicaci�n para param�dicos

- **ComunicacionPaciente.cs**
  - Propiedades:
    - `Comunicacion { get; }` - Interfaz de comunicaci�n para pacientes

### Prop�sito
- **Creaci�n**: Notificar cuando se crean nuevas alertas m�dicas
- **Comunicaci�n**: Registrar y notificar eventos de comunicaci�n (llamadas, mensajes)

---

## 4. PATRON DECORATOR

### Ubicaci�n
- **Funcionalidad**: `Funcionalidad/Mostrar/Decorator/`

### Componentes

#### Interfaces Base
- **IMostrar<T>.cs**
  - `Mostrar(T entidad)` - M�todo base para mostrar cualquier entidad

#### Clases Decorator

##### Clase Base Abstracta
- **MostrarDecorator<T>.cs**
  - Constructor que recibe `IMostrar<T> mostrarComponente`
  - `virtual Mostrar(T entidad)` - Implementaci�n base que delega al componente

##### Decorators Concretos
- **FormatoJsonDecorator<T>.cs**
  - `override Mostrar(T entidad)` - A�ade formato JSON con timestamp
  - Maneja entidades nulas con mensaje de error en JSON

- **FormatoXmlDecorator<T>.cs**
  - `override Mostrar(T entidad)` - A�ade formato XML con timestamp
  - Escapa caracteres especiales para XML v�lido

- **EncabezadoDecorator<T>.cs**
  - Constructor adicional: `(IMostrar<T> mostrarComponent, string titulo, string usuario)`
  - `override Mostrar(T entidad)` - A�ade encabezados profesionales con:
    - T�tulo del reporte
    - Fecha y hora
    - Usuario responsable
    - L�neas separadoras

- **EmergenciaDecorator<T>.cs**
  - `override Mostrar(T entidad)` - A�ade formato especial para emergencias:
    - Para alertas cr�ticas (nivel ? 2): formato de emergencia con asteriscos
    - Para otras entidades: formato de emergencia est�ndar

#### Inyecciones
- **MostradorAdmin.cs**
  - `MostrarCentroMedico { get; }` - Para mostrar centros m�dicos
  - `MostrarSistemaIntegrado { get; }` - Para mostrar informaci�n del sistema
  - `MostrarCuenta { get; }` - Para mostrar cuentas de usuario
  - `MostrarAlerta { get; }` - Para mostrar alertas m�dicas

- **MostradorOperador.cs**
  - `MostrarAlerta { get; }` - Solo puede mostrar alertas

- **MostradorParamedico.cs**
  - `MostrarAlerta { get; }` - Solo puede mostrar alertas

### Prop�sito
A�adir diferentes formatos de visualizaci�n (JSON, XML, encabezados, emergencias) a la informaci�n m�dica sin modificar las clases originales.

---

## 5. PATRON MEMENTO

### Ubicaci�n
- **Funcionalidad**: `Funcionalidad/Actualizar/Memento/`

### Componentes

#### Interfaces
- **IMemento.cs**
  - `ObtenerDescripcion()` - Descripci�n del estado guardado
  - `FechaCreacion { get; }` - Cu�ndo se cre� el memento
  - `UsuarioResponsable { get; }` - Qui�n hizo el cambio

- **IActualizar<T, TCambio>.cs**
  - `Actualizar(T entidad, TCambio cambio)` - M�todo de actualizaci�n gen�rico

#### Clases Memento (Estados Guardados)

##### Para Alertas M�dicas
- **AlertaMemento.cs**
  - Constructor: `(Alerta alerta, string usuarioResponsable)`
  - Estados guardados: tipo, reportante, estado, nivel triaje, fechas, equipo, rutas
  - `RestaurarEstado(Alerta alerta)` - Restaura todos los campos de la alerta
  - `ObtenerDescripcion()` - Descripci�n del estado de la alerta
  - Propiedades: `FechaCreacion`, `UsuarioResponsable`

##### Para Centros M�dicos
- **CentroMedicoMemento.cs**
  - Constructor: `(CentroMedico centro, string usuarioResponsable)`
  - Estados guardados: nombre, tel�fono, complejidad, latitud, longitud
  - `RestaurarEstado(CentroMedico centro)` - Restaura todos los campos del centro
  - `ObtenerDescripcion()` - Descripci�n del estado del centro m�dico
  - Propiedades: `FechaCreacion`, `UsuarioResponsable`

#### Caretaker (Gesti�n de Mementos)
- **HistorialActualizaciones.cs**
  - Constructor: `(int limiteHistorial = 50)`
  - `GuardarEstado(object entidad, IMemento memento)` - Guarda un estado
  - `ObtenerUltimoEstado(object entidad)` - Obtiene el �ltimo estado guardado
  - `PuedeDeshacer(object entidad)` - Verifica si se puede deshacer
  - `DeshacerUltimoCambio(object entidad)` - Deshace el �ltimo cambio
  - `ObtenerHistorialCompleto()` - Obtiene todo el historial
  - `ObtenerHistorialEntidad(object entidad)` - Historial de una entidad espec�fica
  - `LimpiarHistorial()` - Limpia todo el historial
  - `LimpiarHistorialEntidad(object entidad)` - Limpia historial de una entidad

#### Originator (Creaci�n y Restauraci�n)
- **ActualizarConMemento<T, TCambio>.cs**
  - Constructor: `(IActualizar<T, TCambio> actualizadorBase, HistorialActualizaciones historial, string usuarioActual)`
  - `Actualizar(T entidad, TCambio cambio)` - Actualiza guardando estado primero
  - `PuedeDeshacer(T entidad)` - Verifica si se puede deshacer
  - `DeshacerUltimoCambio(T entidad)` - Deshace el �ltimo cambio
  - `CrearMemento(T entidad)` - Crea el memento apropiado seg�n el tipo

#### Servicio Integrado
- **ActualizarSeguroService<T, TCambio>.cs**
  - Constructor: `(IActualizar<T, TCambio> actualizadorBase, HistorialActualizaciones historial, Perfil? usuarioActual)`
  - `Actualizar(T entidad, TCambio cambio)` - Actualizaci�n con validaciones y memento
  - `PuedeDeshacer(T entidad)` - Verifica capacidad de deshacer
  - `DeshacerUltimoCambio(T entidad)` - Deshace cambios
  - `ActualizarConResultado(T entidad, TCambio cambio)` - Actualizaci�n con resultado detallado
  - `TienePermisos(T entidad)` - Validaci�n simple de permisos:
    - Admin: puede actualizar todo
    - Operador: solo alertas
    - Param�dico: solo alertas asignadas a �l

#### Clase de Resultado
- **ResultadoActualizacion<T>.cs**
  - `Exitoso { get; set; }` - Si la actualizaci�n fue exitosa
  - `Entidad { get; set; }` - La entidad actualizada
  - `Mensaje { get; set; }` - Mensaje descriptivo del resultado
  - `PuedeDeshacer { get; set; }` - Si se puede deshacer esta actualizaci�n
  - `FechaActualizacion { get; set; }` - Cu�ndo se realiz� la actualizaci�n

#### Inyecciones
- **ActualizadorAdmin.cs**
  - `ActualizarCentroMedico { get; }` - Para actualizar centros m�dicos
  - `ActualizarCuenta { get; }` - Para actualizar cuentas
  - `ActualizarAlerta { get; }` - Para actualizar alertas
  - `ActualizarEstadoAlerta { get; }` - Para cambiar estado de alertas
  - `ActualizarNivelTriaje { get; }` - Para cambiar nivel de triaje
  - `ActualizarEstadoCuenta { get; }` - Para activar/desactivar cuentas

### Prop�sito
Guardar y restaurar estados anteriores de entidades m�dicas cr�ticas (alertas, centros m�dicos) para permitir reversi�n de cambios importantes en el sistema hospitalario.

---

## ARQUITECTURA GENERAL DEL SISTEMA

### Distribuci�n por Funcionalidad

| Funcionalidad | Patrones Implementados | Total Archivos |
|---------------|------------------------|----------------|
| **Crear** | Factory Method + Observer | 8 archivos |
| **Mostrar** | Decorator + Dependency Injection | 10 archivos |
| **Actualizar** | Memento | 7 archivos |
| **Comunicacion** | Observer + Dependency Injection | 6 archivos |
| **Eliminar** | Dependency Injection b�sico | 2 archivos inyecci�n |
| **Asignar** | Dependency Injection b�sico | 2 archivos inyecci�n |
| **Sistema Global** | Singleton | 1 archivo |

### Estad�sticas de Implementaci�n

**Total de Patrones GoF**: 5 de 23
- **Creacionales**: Singleton, Factory Method
- **Estructurales**: Decorator  
- **Comportamentales**: Observer (doble), Memento

**Total de Interfaces**: 6 interfaces principales
**Total de Clases de Implementaci�n**: 19 clases
**Total de Clases de Inyecci�n**: 7 clases

### Caracter�sticas del Sistema M�dico

**Seguridad**:
- Validaci�n de permisos por rol de usuario
- Historial completo de cambios con responsables
- Reversi�n de actualizaciones cr�ticas

**Flexibilidad**:
- M�ltiples formatos de visualizaci�n
- Notificaciones autom�ticas de eventos
- Creaci�n estandarizada de perfiles

**Escalabilidad**:
- Arquitectura desacoplada con inyecci�n de dependencias
- Patrones extensibles para nuevos tipos de entidades
- Separaci�n clara de responsabilidades

**Aplicaci�n M�dica**:
- Sistema de alertas con niveles de triaje
- Gesti�n de centros m�dicos y equipos
- Control de acceso basado en roles m�dicos
- Auditor�a completa para cumplimiento normativo