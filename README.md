# Sistema Integrado de Alertas Médicas

## SINGLETON
**Clase:** SistemaIntegrado
**Atributos:** instancia, nombre, telefono, l_cuentas, l_centroMedico
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
  - **Atributos:** mostrarComponente
  - **Métodos:** Mostrar(T entidad) ? string
- FormatoJsonDecorator<T>:
  - **Métodos:** Mostrar(T entidad) ? string
- FormatoXmlDecorator<T>:
  - **Métodos:** Mostrar(T entidad) ? string
- EncabezadoDecorator<T>:
  - **Atributos:** titulo, usuario
  - **Métodos:** Mostrar(T entidad) ? string
- EmergenciaDecorator<T>:
  - **Métodos:** Mostrar(T entidad) ? string

## MEMENTO
**Interfaces:**
- IMemento:
  - **Propiedades:** FechaCreacion, UsuarioResponsable
  - **Métodos:** ObtenerDescripcion() ? string
- IActualizar<T, TCambio>:
  - **Métodos:** Actualizar(T entidad, TCambio cambio) ? void

**Servicios:**
- AlertaMemento:
  - **Atributos:** tipoAlerta, reportante, estado, nivelTriaje, fechaCreacion, fechaFinalizacion, equipoAsignado, rutas, FechaCreacion, UsuarioResponsable
  - **Métodos:** 
    - RestaurarEstado(Alerta alerta) ? void
    - ObtenerDescripcion() ? string
- CentroMedicoMemento:
  - **Atributos:** nombre, telefono, complejidad, latitud, longitud, FechaCreacion, UsuarioResponsable
  - **Métodos:**
    - RestaurarEstado(CentroMedico centro) ? void
    - ObtenerDescripcion() ? string
- HistorialActualizaciones:
  - **Atributos:** historialPorEntidad, historialGlobal, limiteHistorial
  - **Métodos:**
    - GuardarEstado(object entidad, IMemento memento) ? void
    - DeshacerUltimoCambio(object entidad) ? IMemento?
    - PuedeDeshacer(object entidad) ? bool
    - ObtenerHistorialCompleto() ? List<IMemento>
    - LimpiarHistorial() ? void
- ActualizarConMemento<T, TCambio>:
  - **Atributos:** actualizadorBase, historial, usuarioActual
  - **Métodos:**
    - Actualizar(T entidad, TCambio cambio) ? void
    - DeshacerUltimoCambio(T entidad) ? bool
    - PuedeDeshacer(T entidad) ? bool
- ActualizarSeguroService<T, TCambio>:
  - **Atributos:** actualizadorConMemento, usuarioActual
  - **Métodos:**
    - Actualizar(T entidad, TCambio cambio) ? void
    - ActualizarConResultado(T entidad, TCambio cambio) ? ResultadoActualizacion<T>
    - DeshacerUltimoCambio(T entidad) ? bool
    - PuedeDeshacer(T entidad) ? bool
