### -------------------------------------
Este repositorio ha sido creado posterior al primero, ya que hubo un problema con el gitIgnore. Si se desea ver el historial completo de commits este se puede ver accediendo al anterior repositorio, llamado "Obsolete".
### -------------------------------------

### ----- Enlaces -----------

Link Tree: https://linktr.ee/SpecialKode  
Trello: https://trello.com/b/0ukCaUtt

### -----------------------

# College and Deans
v 0.1

## 1. Introducción 

### 1.1. Concepto 

Un frenético Roguelike clicker diseñado para ser controlado con un solo clic del ratón o un tap en móvil. El jugador avanzará por varios niveles limpiando salas en las que deberá actuar con rapidez y precisión para derrotar a numerosos enemigos. Todo esto intentando gestionar un límite de tiempo. 

### 1.2. Historia y personajes 

El jugador encarna a un estudiante que empieza una carrera universitaria. En esta universidad, no obstante, el conocimiento se obtiene a base de derrotar a la gente que lo posee para así hacerse con él. 

### 1.3. Propósito, público objetivo y plataformas 

Este juego pretende representar de manera satírica el viaje que supone estudiar una carrera universitaria. Así pues, está dirigido a un público adulto que abarca desde jóvenes estudiantes, alumnos egresados e, incluso, profesores.  

El juego está pensado para jugarse en navegador, ya sea desde PC o móvil. 

## 2. Monetización 

### 2.1. Tipo de modelo de monetización 

El juego generará beneficios de dos formas diferentes: 

La primera y principal, será mediante anuncios que aparecerán al final de cada partida.   

La segunda forma consistirá en la venta de contenido adicional que irá publicándose en los meses siguientes a la salida del videojuego. Este contenido consistirá en nuevas áreas, armas, bonificadores y enemigos. 

### 2.2. Tablas de productos y precios 

Producto 

Descripción 

Precio 

DLC 1 

Nuevos potenciadores y modificadores de arma 

1,99 € 

 DLC 2 

Nuevas áreas, enemigos, potenciadores y modificadores de armas 


5,99 € 

 
## 3. Planificación y Costes 

### 3.1. El equipo humano 

El equipo consta de siete integrantes, tres de ellos centrándose en el diseño del juego y los cuatro restantes dedicándose a la programación de distintas características. 

### 3.2. Estimación temporal del desarrollo 

La evaluación inicial indica que las funcionalidades básicas del juego deberían estar completas en aproximadamente 21 días. Después se espera poder completar el juego en otros 21 días. 

### 3.3. Costes asociados 

El coste esperado de las labores básicas de diseño es de aproximadamente 48 horas por diseñador, pudiendo incrementarse para añadir nuevo contenido, refinar, testear o balancear el existente. 

En cuanto a la implementación de las funcionalidades básicas se estima que requieran alrededor de 100 horas por desarrollador. De nuevo, este número puede no ser definitivo. 

## 4. Mecánicas de juego y elementos de juego 

### 4.1. Descripción detallada del concepto de juego 

El jugador deberá explorar una serie de niveles compuestos por distintas salas en las que tendrá que enfrentarse a grupos de enemigos. Durante la exploración se le presentarán oportunidades de mejorar sus características y equipo. En cada nivel deberá encontrar y derrotar a un jefe dentro de un límite de tiempo. Una vez haya completada esta tarea, podrá acceder al siguiente nivel. Tras superar cuatro niveles el jugador se enfrentará al jefe final del juego. En caso de que el jugador se quede sin tiempo, deberá comenzar desde el principio. 

### 4.2. Descripción detallada de las mecánicas de juego 

El personaje se controlará únicamente con el ratón. Para moverse habrá que clicar en el punto de la sala a donde se desea desplazarse. En caso de chocar con un obstáculo, el personaje se detendrá. Si el jugador hace doble clic sobre una posición, el personaje se lanzará hacia esa ubicación con rapidez, evitando el daño que pudiera sufrir durante la acción.  

El personaje contará con un arma a distancia para enfrentarse a sus enemigos. Esta arma podrá modificarse a lo largo de la partida pudiendo alterar sus características. Para atacar el jugador deberá clicar sobre el enemigo objetivo.  

Al derrotar enemigos el jugador podrá obtener dinero que podrá gastar para conseguir mejoras en la cafetería. 

Cada nivel debe completarse en un tiempo límite. El jugador conservará el tiempo restante de los cuatro niveles previos al jefe final y este se sumará al tiempo base disponible para derrotar al jefe final. En caso de recibir un golpe, el tiempo disponible en el nivel se reducirá. Si el tiempo se agota, el jugador perderá la partida y tendrá que volver a empezar.   

### 4.3. Comportamiento de los enemigos 

Los enemigos se pueden dividir en comunes y jefes.  

Dentro de los comunes habrá dos patrones básicos de comportamiento: 

Enemigos que detectan la posición del jugador y van a por él para realizar ataques cuerpo a cuerpo. 

Enemigos que detectan la posición del jugador y abren fuego contra él. Mientras disparan no se mueven. Sin embargo, entre cada periodo de disparo, estos cambiarán de posición. 

En cuanto a los jefes, estos son bastante más poderosos que los enemigos comunes en cuanto a daño y resistencia. Una vez el jugador se enfrente a uno de ellos este comenzará a desplazarse por la sala de acuerdo a su propio patrón de movimiento, y comenzará a atacar eligiendo los ataques de una lista de forma aleatoria. Cada jefe contará con su propia lista. 

### 4.4. Controles 

Ordenador: 

Click izquierdo: moverse a la posición designada. 

Click izquierdo sobre enemigo: atacar al enemigo. 

Doble click izquierdo: acción evasiva hacia la posición designada. 

Móvil: 

Tap: moverse a la posición designada. 

Tap sobre un enemigo: atacar al enemigo. 

Doble Tap: acción evasiva hacia la posición designada. 

### 4.5. Niveles y misiones 

Los niveles se componen de un número aleatorio de salas con una disposición también aleatoria para cada partida. Cada sala contará con un grupo de enemigos que deberán ser derrotados antes de poder proceder a la siguiente. Una vez el jugador acabe con los enemigos, estos no volverán a aparecer cuando vuelva a pasar por la sala.  

Una sala en cada nivel tendrá un objeto potenciador aleatorio. También existe la posibilidad de que una de estas salas sea la cafetería. En ella el jugador podrá gastar dinero a cambio de distintas mejoras y bonificadores. 

Cada nivel contará con un jefe que protegerá el acceso al siguiente nivel. Tras derrotarlo el jugador obtiene algo. 

### 4.5. Objetos, armas y power ups. 

El arma del jugador cuenta con un espacio de modificación. Los modificadores de arma alterarán de manera significativa las características del arma y podrán encontrarse explorando los niveles, comprándolos en la cafetería o derrotando jefes. 

El jugador cuenta con un arma a distancia que en su forma básica dispara proyectiles cada x tiempo. No obstante, esta puede ir variando su comportamiento dependiendo de los distintos modificadores disponibles. 

Modificador 

Efecto 

M1 

El tiempo entre proyectiles disminuye, lo que permite disparar más por segundo. Reduce un poco el daño. 

M2 

Aumenta el daño de los proyectiles, pero aumenta el tiempo entre ellos. 

M3 

Cambia los proyectiles por bombas que hacen daño en área. Aumenta considerablemente el tiempo entre ellos. 

M4 

Dispara en forma de rayo continuo. Reduce el daño. 

M5 

Dispara en forma de onda hacia el frente. Tiene un menor alcance que los proyectiles normales pero un mayor rango. 

M6 

Dispara dos proyectiles en vez de uno. Aumenta un pelín el tiempo entre ellos. 

M7 

Modifica el disparo para que ahora sean ondas que se desprenden del arma en todas direcciones. Es de corto alcance. 

M8 

El proyectil rebota una vez en las paredes y columnas. 

M9 

El proyectil atraviesa al enemigo en lugar de destruirse. 

M10 

Reduce el daño de los proyectiles a la mitad. 

De manera similar, el personaje podrá obtener distintos potenciadores. A diferencia de los modificadores de arma, estos potenciadores son acumulables y sus efectos son más variados. 

 

Potenciador 

Efecto 

P1 

Aumenta la velocidad. 

P2 

Ralentiza el paso del tiempo. 

P3 

Reduce el tiempo perdido por recibir un golpe. 

P4 

Duplica el daño infligido y el tiempo perdido por golpe recibido. 

P5 

Reduce la velocidad de movimiento. 

P6 

Aumenta el paso del tiempo. 

P7 

Muestra el mapa del nivel completo junto con los objetos y salas relevantes. 

P8 

Muestra la vida de los enemigos. 

P9 

Oculta el mapa del nivel. El efecto desaparece al coger otro potenciador. 

P10 

Oculta el tiempo restante del nivel. El efecto desaparece al coger otro potenciador. 

P11 

Aumenta el daño. 

## 5. Trasfondo 

### 5.1. Descripción detallada de la historia 

Corre el año 2030, nos encontramos en un futro distópico en el que la gente sin recursos tiene muy complicado acceder a la educación universitaria, por lo que se ven obligados a utilizar la violencia como aliada. Nuestro protagonista acaba de aprobar la selectividad, y con ilusión se dispone a comenzar una nueva etapa, la universidad. Pero aun creyendo saber a lo que se va a enfrentar, no es consciente de lo que le espera. Está solo, no conoce a nadie y no tiene experiencia en combate. Así, comienza el viaje y tras mucho esfuerzo, dolor, golpes y disparos consigue graduarse.  

### 5.2. Personajes 

El protagonista y personaje al que el jugador controla es un chico que acaba de entrar a la universidad. No tiene ni idea del reto que esto supone y todavía no sabe muy bien lo que quiere hacer a pesar de haber escogido una carrera. 

Además del protagonista, existen dos tipos de personajes más, enemigos y aliados. 

Dentro de los aliados encontramos al camarero de la cafetería, un hombre de mediana edad al que no parece sorprenderle toda la violencia que se desprende diariamente en la universidad. Lo único que quiere es acabar su jornada e irse a casa. No es muy hablador. 

En cuanto a los enemigos, dentro de estos podemos distinguir entre dos tipos, los jefes y los enemigos comunes. 

Los enemigos comunes son empleados de la universidad, algunos profesores y otros alumnos de distintas carreras y cursos a quienes los jefes envían a detener a cualquiera que intente llegar a ellos. 

Por otro lado, los jefes son entes superiores, profesores que realizan trabajos de investigación y son expertos en sus respectivos campos. Esperan en sus aulas o laboratorios a que los alumnos dignos de su atención les encuentren.  

### 5.3. Entornos y lugares 

El juego se desarrolla únicamente en el interior de la universidad. Dentro de la misma, se podrán recorrer los siguientes lugares: 

Aulas: Sala básica y principal de la universidad. En ella se pueden encontrar mesas, sillas, pizarras, pósteres y folletos en las paredes, mochilas y demás enseres típicos de las aulas universitarias. 

Laboratorios: Junto a las aulas, las salas más básicas. En ellos, además de todo lo mencionado para las aulas se podrán encontrar pizarras móviles y aparatos especializados de distintos campos. 

Cafetería: Sala de “descanso”. En ella no habrá enemigos, únicamente el camarero. Se compone de unas cuantas mesas y sillas, además de la barra y un par de máquinas expendedoras. 

En todas las salas excepto en la cafetería, podrán aparecer obstáculos tales como columnas y agujeros en el suelo. 

## 6. Arte 

### 6.1. Estética general del juego 

El estilo artístico que va a seguir el arte del juego es la estética pixel art, debido a que es el estilo con el que el equipo de arte se siente más cómodo.  

### 6.2. Apartado visual 

El juego presentará elementos propios de una universidad que se utilizarán en los escenarios y personajes, algunos exagerados para que sean coherentes con el contexto del juego. Dado que la acción del juego transcurre en una universidad, los menús e interfaz adaptarán elementos típicos del entorno de un estudiante para tener una estética acorde. 

### 6.3. Música 

El juego contará con dos temas principales, uno que sonará en bucle mientras el jugador está en el menú o explora los niveles y otro que se escuchará durante las peleas con los jefes. 

### 6.4. Ambiente sonoro 

En cuanto al resto de sonidos del juego habrá una variedad de sonidos de ataque para los enemigos y el propio jugador, un sonido para cuando el jugador compre algo en la cafetería y un sonido para cuando el jugador reciba daño. 

## 7. Interfaz 

### 7.1. Diseños básicos de los menús 

Menú Principal 

Ilustración 1: Menú Principal 

 

Ilustración 2: Menú de Opciones 

 

Ilustración 3: Menú de Pausa 

 

Ilustración 4: Pantalla de creditos y reconocimientos 

### 7.2. Interfaz de usuario 

 

Ilustración 5: HUD durante el juego 

### 7.3. Diagrama de flujo 

 

Ilustración 6: Diagrama de Flujo 

## 8. Hoja de ruta del desarrollo  

### 8.1. Funcionalidad básica y GDD 

17 – 10 - 21 

### 8.2. Alpha 

31 – 10 -21 

### 8.3. Beta 

21 – 11- 21 

### 8.4. Fecha de lanzamiento 

12 – 12 - 21 

