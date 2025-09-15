# Sistema Integrado de Alertas Médicas

## SINGLETON
**Clase:** SistemaIntegrado
**Atributos:** SistemaIntegrado? instancia, string nombre, int telefono, List<Cuenta> l_cuentas, List<CentroMedico> l_centroMedico
**Métodos:** 
- GetInstance() ? SistemaIntegrado

## FACTORY METHOD
**Interface:** IPerfilFactory
**Métodos:**
- CrearPerfil(string nombre, string correo, string tipoCedula, uint cedula, uint celular, string contrasena, params object[] extras) ? Perfil

**Servicios:**
- AdminFactory: 
  - **Métodos:** CrearPerfil(string nombre, string correo, string tipoCedula, uint cedula, uint celular, string contrasena, params object[] extras) ? Perfil
- OperadorFactory:
  - **Métodos:** CrearPerfil(string nombre, string correo, string tipoCedula, uint cedula, uint celular, string contrasena, params object[] extras) ? Perfil
- ParamedicoFactory:
  - **Métodos:** CrearPerfil(string nombre, string correo, string tipoCedula, uint cedula, uint celular, string contrasena, params object[] extras) ? Perfil
- PacienteFactory:
  - **Métodos:** CrearPerfil(string nombre, string correo, string tipoCedula, uint cedula, uint celular, string contrasena, params object[] extras) ? Perfil

## OBSERVER
**Interfaces:**
- ICreacionObserver<T>:
  - **Métodos:** OnElementoCreado(T elemento) ? string
- INotificacionObserver:
  - **Métodos:** OnNotificacionEnviada(string mensaje, Perfil destinatario) ? string

**Servicios:**
- AlertaCreacionObserver:
  - **Métodos:** OnElementoCreado(Alerta elemento) ? string
- LogComunicacionObserver:
  - **Métodos:** OnNotificacionEnviada(string mensaje, Perfil destinatario) ? string

## DECORATOR
**Interface:** IMostrar<T>
**Métodos:**
- Mostrar(T entidad) ? string

**Servicios:**
- MostrarDecorator<T>:
  - **Atributos:** IMostrar<T> mostrarComponente
  - **Métodos:** Mostrar(T entidad) ? string
- FormatoJsonDecorator<T>:
  - **Métodos:** Mostrar(T entidad) ? string
- FormatoXmlDecorator<T>:
  - **Métodos:** Mostrar(T entidad) ? string
- EncabezadoDecorator<T>:
  - **Atributos:** string titulo, string usuario
  - **Métodos:** Mostrar(T entidad) ? string
- EmergenciaDecorator<T>:
  - **Métodos:** Mostrar(T entidad) ? string

## MEMENTO
**Interfaces:**
- IMemento:
  - **Propiedades:** DateTime FechaCreacion, string UsuarioResponsable
  - **Métodos:** ObtenerDescripcion() ? string
- IActualizar<T, TCambio>:
  - **Métodos:** Actualizar(T entidad, TCambio cambio) ? void

**Servicios:**
- AlertaMemento:
  - **Atributos:** string tipoAlerta, Perfil reportante, bool estado, uint nivelTriaje, DateTime fechaCreacion, DateTime fechaFinalizacion, Perfil? equipoAsignado, List<Ruta> rutas, DateTime FechaCreacion, string UsuarioResponsable
  - **Métodos:** 
    - RestaurarEstado(Alerta alerta) ? void
    - ObtenerDescripcion() ? string
- CentroMedicoMemento:
  - **Atributos:** string nombre, int telefono, string complejidad, float latitud, float longitud, DateTime FechaCreacion, string UsuarioResponsable
  - **Métodos:**
    - RestaurarEstado(CentroMedico centro) ? void
    - ObtenerDescripcion() ? string
- HistorialActualizaciones:
  - **Atributos:** Dictionary<object, Stack<IMemento>> historialPorEntidad, List<IMemento> historialGlobal, int limiteHistorial
  - **Métodos:**
    - GuardarEstado(object entidad, IMemento memento) ? void
    - DeshacerUltimoCambio(object entidad) ? IMemento?
    - PuedeDeshacer(object entidad) ? bool
    - ObtenerHistorialCompleto() ? List<IMemento>
    - LimpiarHistorial() ? void
- ActualizarConMemento<T, TCambio>:
  - **Atributos:** IActualizar<T, TCambio> actualizadorBase, HistorialActualizaciones historial, string usuarioActual
  - **Métodos:**
    - Actualizar(T entidad, TCambio cambio) ? void
    - DeshacerUltimoCambio(T entidad) ? bool
    - PuedeDeshacer(T entidad) ? bool
- ActualizarSeguroService<T, TCambio>:
  - **Atributos:** ActualizarConMemento<T, TCambio> actualizadorConMemento, Perfil? usuarioActual
  - **Métodos:**
    - Actualizar(T entidad, TCambio cambio) ? void
    - ActualizarConResultado(T entidad, TCambio cambio) ? ResultadoActualizacion<T>
    - DeshacerUltimoCambio(T entidad) ? bool
    - PuedeDeshacer(T entidad) ? bool
