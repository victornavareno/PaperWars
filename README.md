![image](https://github.com/user-attachments/assets/41a14cd1-dddb-4047-bc2d-cd6042bfd897)

#  **PAPER WARS en Unity**  

Este repositorio contiene los archivos C# desarrollados como parte de un proyecto para aprender Unity y diseño de juegos interactivos en Imagen Digital. El objetivo principal fue comprender la interacción entre objetos, las mecánicas de juego básicas y la transición entre escenas.


##  **Archivos incluidos y sus funcionalidades**  

### 1. **`Weapon.cs`**  
Este script gestiona el sistema de armas del jugador:  
- Controla la creación de proyectiles (balas) al presionar un botón de disparo.  
- Configura la velocidad y dirección de las balas, asegurando que se disparen hacia adelante desde la posición del jugador.  
- Añade realismo al incluir tiempos de recarga entre disparos.  


### 2. **`PlayerMovement.cs`**  
Responsable del movimiento del jugador:  
- Maneja la rotación y el desplazamiento del jugador, permitiendo un control fluido mediante las teclas de dirección.  
- Implementa una mecánica de nitro para acelerar temporalmente al soltar la barra espaciadora, añadiendo dinamismo al movimiento.  
- Gestiona las colisiones con otros objetos:  
  - Si el jugador colisiona con un enemigo, se activa una animación de explosión y se muestra un mensaje de "Game Over".  
  - Interactúa con otros objetos como "obstáculos móviles" de forma natural (por ejemplo, empujándolos con el collider).  


### 3. **`EnemyMovement.cs`**  
Controla el comportamiento de los enemigos:  
- Los enemigos persiguen al jugador usando cálculos de rotación suaves para mantener un movimiento realista.  
- Al ser alcanzados por las balas, se activa un efecto de explosión y se notifica al `GameManager` que han sido eliminados.  
- Incluye sonidos y partículas para enriquecer la experiencia visual y auditiva.

---

#### Pequeña preview del juego (Los enemigos nos siguen siempre):
![image](https://github.com/user-attachments/assets/98fc0246-80d0-49c4-91a5-4bc644f27560)

---

### 4. **`Bullet.cs`**  
Este script define el comportamiento de las balas disparadas por el jugador:  
- Controla la velocidad y dirección de las balas una vez creadas.  
- Detecta colisiones con enemigos, activando sus respectivos efectos.  
- Destruye las balas tras un tiempo predeterminado para optimizar el rendimiento del juego.  


### 5. **`GameManager.cs`**  
El núcleo de la lógica del juego, encargado de coordinar las principales mecánicas:  
- Lleva un registro del número total de enemigos en la escena y de cuántos han sido eliminados.  
- Cambia de nivel (o escena) cuando todos los enemigos han sido derrotados.  
- Muestra un mensaje de "¡Buen trabajo!" al jugador antes de la transición, añadiendo un pequeño respiro tras completar un nivel.  

### 6. **`MainMenu.cs`**  
Gestor del menú principal del juego:  
- Permite al jugador iniciar el juego o salir de la aplicación.  
- Cambia a la escena del primer nivel al presionar "Start".  
- Una base simple, pero funcional, para futuros proyectos más complejos.  

![image](https://github.com/user-attachments/assets/f607de7a-f486-4e03-bd9f-e4130721a2ba)


### AUTOR: Victor Navareño para Imagen Digital, Ingeniería Informática en Ingeniería del Software
