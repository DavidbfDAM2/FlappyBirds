# FlappyBirds

Joc arcade 2D desenvolupat amb **Unity i C#**, inspirat en Flappy Bird. Mantén l'ocell en vol, ajusta la velocitat per esquivar obstacles i enemics, i recull cuques de llum per sumar punts.

El repte és doble: aconseguir la **millor puntuació** i superar el **temps màxim de supervivència**.

> Projecte d'aprenentatge de desenvolupament de videojocs. Aquest document descriu el codi actual.

## Com es juga

L'ocell avança automàticament. Cada pulsació de vol aplica una força cap amunt; entre impulsos, la gravetat el fa baixar.

| Element | Efecte |
| --- | --- |
| Cuques de llum grogues | Sumen **10 punts** per recollida. |
| Papallones | Recuperen **una vida**, fins a un màxim de cinc. Només es generen quan falta vida. |
| Ocells enemics | Resten **una vida** en tocar el jugador. S'activen quan aquest s'hi acosta. |
| Obstacles, terra i sostre | Una col·lisió sòlida provoca **Game Over**, independentment de les vides restants. |

Quedar-se sense vides també acaba la partida. Superar barreres incrementa el comptador d'obstacles, separat de la puntuació.

### Controls

| Acció | Control |
| --- | --- |
| Impuls cap amunt | **W**, **fletxa amunt** o **clic esquerre** |
| Accelerar | Mantenir **D** o **fletxa dreta** |
| Frenar sense retrocedir | Mantenir **A** o **fletxa esquerra** |
| Recuperar la velocitat normal | Deixar anar els controls horitzontals |
| Pausar o continuar | **Esc**, durant la partida |
| Començar des del panell inicial | **Enter** o botó de començar |
| Reiniciar des de Game Over | **Enter** o botó de reiniciar |

## Menús i flux de partida

1. **Inici:** mostra les instruccions i manté el joc aturat fins que el jugador decideix començar.
2. **Partida:** el HUD mostra vides, punts, obstacles superats i temps de joc.
3. **Pausa:** permet continuar, reiniciar o sortir.
4. **Game Over:** mostra els resultats, els dos rècords de partida i la indicació per tornar a jugar.

En reiniciar des de la pausa o des de Game Over, es carrega una partida nova **directament**, sense tornar a mostrar les instruccions inicials.

La pausa atura el temps de joc i la física; no suma temps de supervivència. Esc no permet saltar-se el panell inicial ni el de Game Over.

> **Sortir:** utilitza `Application.Quit()`. Tanca una compilació d'escriptori, però no atura el mode Play de l'Editor ni tanca la pestanya d'una versió web.

## Funcionalitats

- Moviment amb física 2D, gravetat i impulsos verticals.
- Avanç automàtic amb acceleració i frenada temporals.
- Càmera amb desplaçament horitzontal suau: l'ocell queda més endavant en accelerar i més enrere en frenar.
- Obstacles amb moviment vertical i generació a altures variables.
- Intervals aleatoris, configurables des de l'Inspector, per generar obstacles, enemics i cuques de llum.
- Generació de papallones condicionada a la necessitat de recuperar vida.
- Reciclatge de trams de terra i sostre per mantenir l'avanç continu.
- Eliminació d'elements generats que queden enrere.
- Gestió de menús mitjançant un `enum` d'estats: `Start`, `Playing`, `Paused` i `GameOver`.
- Rècords locals persistents de puntuació i supervivència.

## Rècords i desament

Els dos rècords són **independents**: no cal aconseguir-los en la mateixa partida. Es consulten i s'actualitzen en arribar a Game Over amb `PlayerPrefs`.

| Clau | Tipus | Contingut |
| --- | --- | --- |
| `BestScore` | `int` | Puntuació més alta aconseguida. |
| `BestSurvivalTime` | `float` | Temps màxim sobreviscut, en segons. |

El temps es mostra amb el format `HH:MM:SS`. Reiniciar una partida no esborra els rècords.

En Windows, `PlayerPrefs` desa les dades al registre. En WebGL, Unity utilitza IndexedDB del navegador: els rècords són locals al navegador i perfil, no se sincronitzen entre dispositius i es poden perdre si s'esborren les dades del lloc web. No és un sistema protegit contra manipulacions ni un rànquing en línia.

## Obrir el projecte

### Requisits

- **Unity Hub**.
- **Unity 6000.3.24f1**, versió indicada a `ProjectSettings/ProjectVersion.txt`.
- **Git i Git LFS** per obtenir correctament els recursos gràfics del repositori.
- Per generar una versió web: mòdul **Web Build Support** de la mateixa versió de Unity.

### Recuperar els recursos LFS

Els PNG sota `Assets/` estan configurats amb Git LFS. Després de clonar el repositori amb el teu client Git, executa aquestes ordres a l'arrel del projecte si els recursos no s'han descarregat automàticament:

```bash
git lfs install
git lfs pull
```

Per consultar els fitxers gestionats amb LFS:

```bash
git lfs ls-files
```

Una descàrrega ZIP no sempre inclou els continguts reals dels fitxers LFS; és preferible treballar amb una còpia clonada del repositori.

### Executar a l'Editor

1. A Unity Hub, afegeix la carpeta que conté **`Assets/`, `Packages/` i `ProjectSettings/`**.
2. Obre-la amb Unity **6000.3.24f1** i espera que s'importin els recursos i es resolguin els paquets.
3. Obre [`Assets/Scenes/MainScene.unity`](../Assets/Scenes/MainScene.unity).
4. Prem **Play** i dona el focus a la finestra **Game**.
5. Prem **Enter** o el botó de començar.

Els controls dels scripts utilitzen l'API clàssica `UnityEngine.Input`. Si canvies la configuració d'entrada del projecte, mantén habilitat **Input Manager (Old)** o **Both** a **Active Input Handling**.

## Organització del projecte

Les rutes següents són relatives a l'arrel del projecte Unity:

| Ruta | Contingut |
| --- | --- |
| `Assets/Scenes/MainScene.unity` | Escena principal. |
| `Assets/Scripts/` | Lògica de joc, física, càmera, interfície i generadors. |
| `Assets/Prefabs/` | Objectes reutilitzables del joc. |
| `Assets/Sprites/` | Recursos gràfics 2D. |
| `Packages/` | Dependències gestionades per Unity. |
| `ProjectSettings/` | Configuració i versió del projecte. |
| `docs/` | Fitxers de la compilació web existent. |

### Scripts principals

| Script | Responsabilitat |
| --- | --- |
| `GameManager.cs` | Estats, panells, pausa, reinici, resultats i desament dels rècords. |
| `BirdPhysics.cs` | Controls, velocitat, vides, puntuació i detecció de col·lisions. |
| `GameHUD.cs` | Marcador i cronòmetre de la partida. |
| `CameraScript.cs` | Seguiment i suavitzat de l'enquadrament horitzontal. |
| `ObstacleSpawner.cs` | Generació d'obstacles amb intervals aleatoris. |
| `EnemySpawner.cs` | Generació d'enemics amb intervals aleatoris. |
| `ScorePickupSpawner.cs` | Generació de cuques de llum amb intervals aleatoris. |
| `ButterflySpawner.cs` | Generació de papallones quan falta vida. |
| `EnemyBirdPhysics.cs` | Activació, moviment i reacció dels enemics al contacte. |
| `ObstaclePhysics.cs` | Moviment dels obstacles i registre del pas del jugador. |
| `GroundRecycler.cs` | Reciclatge dels trams de terra i sostre. |

## Ajustar la dificultat

Els camps exposats a l'Inspector permeten ajustar les velocitats de l'ocell, la força de vol, els intervals de generació, les altures dels obstacles i el comportament de la càmera i dels enemics.

- Mantén els intervals de generació **positius**, amb el mínim menor o igual que el màxim.
- Per conservar la frenada sense retrocés, utilitza velocitats positives i ordenades: lenta, normal i ràpida.
- Els valors desats a les escenes i als prefabs poden ser diferents dels valors inicials dels scripts.
- Prova les combinacions d'obstacles i enemics tant accelerant com frenant: la separació temporal no equival a una distància fixa.

## Estat i possibles millores

La base jugable inclou inici, pausa, Game Over, resultats i rècords persistents. Els **efectes de so** queden pendents.

Possibles ampliacions: feedback visual en rebre mal, dificultat progressiva i comprovacions per evitar aparicions superposades.