# FlappyBirds

Joc 2D fet amb Unity en què controles un ocell que avança automàticament. Aleteja per esquivar obstacles i enemics, recull objectes i intenta superar tantes barreres com puguis.

> El projecte Unity és a la subcarpeta [`FlappyBirds/`](FlappyBirds/). Aquesta carpeta també conté altres projectes independents.

## Obrir i jugar

1. Obre **Unity Hub** i afegeix la carpeta `FlappyBirds/` com a projecte (no aquesta carpeta pare).
2. Obre'l amb **Unity 6000.3.24f1**.
3. Carrega `FlappyBirds/Assets/Scenes/MainScene.unity` i prem **Play**.
4. Fes **clic amb el ratolí** per aletejar. L'ocell avança sol cap a la dreta.

## Funcionament

- Les barreres oscil·len verticalment i apareixen davant de la càmera a altures variables. Superar-ne una augmenta el comptador d'obstacles.
- Els enemics s'activen quan l'ocell s'hi acosta i li resten una vida en tocar-lo.
- Els pickups de punts augmenten la puntuació; les papallones recuperen una vida fins al màxim.
- El HUD mostra vides, puntuació, temps de partida i obstacles superats.
- Un xoc amb un obstacle sòlid o quedar-se sense vides reinicia l'escena.
- El terra i el sostre reutilitzen dos trams cadascun per permetre avançar contínuament. Els obstacles, enemics i pickups generats s'eliminen quan queden enrere.

## Organització

| Ruta dins de `FlappyBirds/` | Contingut |
| --- | --- |
| `Assets/Scenes/MainScene.unity` | Escena principal i configuració dels objectes. |
| `Assets/Scripts/` | Control de l'ocell, càmera, HUD, moviment, reciclatge i generadors. |
| `Assets/Prefabs/` | Prefabs d'obstacles, enemics, pickups, terra i sostre. |
| `Assets/Sprites/` | Recursos gràfics 2D. |

## Estat del projecte

Els efectes de so queden pendents. La dificultat dels obstacles es pot ajustar des del prefab `obstacle_0` i els paràmetres de l'objecte `obstacleSpawner` a l'escena.
