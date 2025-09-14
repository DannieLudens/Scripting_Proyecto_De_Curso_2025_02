# Scripting_Proyecto_Del_Curso_Scripting_2025_02

## Documentación

Gestión, y documentación a traves de Notion
[Link al notion](https://www.notion.so/Proyecto-de-curso-scripting-244fa57617cb808cba7bf1f2ecfbcc97?source=copy_link)

Reuniones los viernes a las 2 p.m. en Teams

Miembros del equipo

- Sofía Lezcano Arenas
- Valeria Cecilia Blanquicett Angulo
- Daniel Esteban Ardila Alzate

---

<details>
  <summary>Propuesta de Proyecto (inicial)</summary>

## Propuesta de Proyecto


### Alcance del Proyecto 

Este proyecto consiste en el desarrollo de un juego de plataformas en 2D con un único nivel. El jugador controla un personaje que debe atravesar un entorno lleno de obstáculos, plataformas móviles y enemigos básicos para llegar a una meta.

Durante el juego, se implementará una mecánica de salto, recolección de objetos y vida limitada. El objetivo es aplicar al menos un patrón de diseño (como el pa-trón State para los enemigos o Factory para la creación de objetos interactivos), una estructura de datos distinta a listas o arreglos (por ejemplo, una cola o un diccionario para gestionar eventos u objetos recogidos) y pruebas unitarias de al menos una funcionalidad (como la lógica de recolección o el conteo de vidas).

El juego tendrá una interfaz básica de inicio, instrucciones y botón de reinicio. Aunque el nivel es único, se garantizará un gameplay completo y funcional.

### Miembros del Equipo 

- Sofia Lezcano Arenas
- Valeria Cecilia Blanquicett Angulo
- Daniel Esteban Ardila Alzate

### Herramientas que Usaremos

- Motor de desarrollo: Unity 6 (2D)
- Lenguaje de programación: C#
- Entorno de desarrollo: Visual Studio Code o Visual Studio 2022
- Control de versiones: Git + GitHub

### Herramientas adicionales:

  - Unity Test Framework para pruebas unitarias
  - Assets gratuitos de Unity Asset Store para los gráficos y sonidos
  - Trello o Google Drive para la organización de tareas y archivos compartidos

</details>

<details>
  <summary>Propuesta de Proyecto (Corregida)</summary><br>

**Nombre del Proyecto:**

**“Neon Heist”** – Juego de plataformas 2D con enemigos reactivivos y mecánicas de recolección.

**Alcance del Proyecto**

Este proyecto consiste en el desarrollo de un videojuego de plataformas 2D titulado **"Neon Heist"**, con un único nivel cuidadosamente diseñado para ofrecer una experiencia de gameplay completa. El jugador controla a **Jayce**, un personaje que debe avanzar hasta una meta final superando diversos retos.

El juego incluirá las siguientes mecánicas principales:

- **Salto y movimiento lateral.**
- **Recolección de objetos de tres tipos**:
	- **Billetes**: suman puntos.
	- **Tarjetas**: desbloquean puertas.
	- **Botella de bebida Jayce**: recuperan vida.
- **Sistema de vidas**:
	- El jugador comienza con **3 vidas**.
	- Cada golpe recibido reduce media vida.
	- Al perder todas las vidas, el jugador pierde la partida.
	- Si el jugador tiene las vidas completas y pasa por una botella de bebida Jayce, no puede recogerla.
- **Obstáculos y enemigos**:
	- El entorno tendrá **20 obstáculos**, divididos en:
		- **Plataformas móviles (10)**.
		- **Trampas fijas (5)** entre sierras y pozos.
		- **Trampas activas (5)** entre caída de cajas y una prensa que cae al suelo, que se activan cuando el personaje pasa por cierta zona.
	- **2 tipos de enemigos**:
		- **Patrulleros**: un robot vigilante que causa daño cuando se acerca.
		- **Reactivos**: un robot que vuela y tira bombas cuando detecta al personaje, y un robot que golpea al personaje cuando está cerca.

El objetivo principal es **llegar a la oficina del jefe de la empresa**, mediante la recolección de tarjetas en el camino para desbloquear puertas. Los billetes aumentan el puntaje final, incentivando la exploración. Las bebidas de Jayce permiten al jugador mantenerse con vida más tiempo.

**Aplicación de Conceptos del Curso**

**Patrones de diseño**

Se aplicarán los siguientes patrones:

- **State**: Para manejar los estados de los enemigos reactivos (ej. el robot volador pasa de “patrullando” a “atacando” al detectar al jugador).
- **Factory Method**: Para instanciar de forma flexible enemigos y obstáculos con comportamiento variable sin acoplar directamente su creación.
- **Observer (o Event Manager personalizado)**: Para notificar cambios como:
	- Recolección de objetos.
	- Cambios en la vida del jugador.
	- Activación de trampas cuando el jugador pasa por cierta zona.

**Estructuras de datos**

- **Diccionario** Para manejar inventario o conteo de objetos (billetes, tarjetas, bebidas).
- **Cola** Para manejar eventos pendientes del sistema de juego (ej. se activa una trampa, se recolecta una tarjeta, se actualiza la UI, etc.).

**Pruebas unitarias**

Usaremos **Unity Test Framework** para validar:

- El comportamiento del sistema de vidas.
- El sistema de recolección de objetos.
- Interacciones con obstáculos.
- Comportamiento de enemigos en estados definidos.

**Miembros del Equipo**

- Sofía Lezcano Arenas
- Valeria Cecilia Blanquicett Angulo
- Daniel Esteban Ardila Alzate

**Herramientas que Usaremos**

- **Motor de desarrollo:** Unity 6.1 (6000.1.13f1) 2D
- **Lenguaje de programación:** C#
- **Entorno de desarrollo:** Visual Studio 2022
- **Control de versiones:** GitHub
- **Organización y planeación:** Notion para la organización

### Herramientas adicionales:

• Unity Test Framework para pruebas unitarias
• Assets gratuitos de Unity Asset Store para los gráficos y sonidos
• Trello, Notion o Google Drive para la organización de tareas y archivos
compartidos
</details>

---

## Licencias

- Sprites del Proyecto de uso gratuito de [craftpixel.net](https://craftpix.net/freebies/free-industrial-zone-tileset-pixel-art/)

- [Detalles de Licencia](https://craftpix.net/file-licenses/) para los Sprites usados en este proyecto
