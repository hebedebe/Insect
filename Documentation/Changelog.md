---
title: Changelog
author: Oliver Alcaraz
geometry: margin=2cm
output: pdf_document
---

## Note:
On the 14th of June, the USB containing the project was corrupted, along with all previous documentation, art assets and the changelog. As a result, progress before the 14th is unrecoverable.

### Changelogs

#### Log 14/06/2022
* All previous progress was lost
* Started work on remaking the project
* Created empty `URP` project in `Unity 2020.3.34f`
* Created placeholder player sprite
* Created centipede enemy sprites
* Created `PlayerController` script
* Created `CentipedeController` script
* Created player movement
* Created player dash particles
* Created player dash
* Added camera post processing effects
* Salvaged old centipede code from an old version of the documentation that was uploaded to the cloud
* Added player health and max health variables
* Added player currency (`chips`) variable
* Created `CameraController` script
* Implemented camera shake
* Camera shake on dash

#### Log 15/06/2022
* Tuned player movement
* Fixed centipede sprites
* Tuned player dash
* Improved player dash particles
* Created blood particle sprites
* Created player "health" sprite (battery)
* Created `Enemy` tag and applied it to the centipede
* Player is damaged when in contact with a `trigger collider` that has the `Enemy` tag
* Player temporary invulnerability after being damaged
* Implemented player damage effects
* Player emits blood particles upon being damaged
* Camera shake when player is damaged

#### Log 16/06/2022
* Created player attack animation
* Player attack can damage enemies
* Created `EnemySpawner` script
* Created `GameSetup` script that enables vsync on game start
* Changed player blood particle effects to soul particles
* Player dash particles are now white
* Added a white translucent trail to the player
* Created 