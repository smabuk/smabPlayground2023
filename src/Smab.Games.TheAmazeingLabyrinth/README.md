# The aMAZEing Labyrinth

## Purpose
Model the game _The aMAZEing Labyrinth_ using c# and Blazor.

## Functionality
1. Drag edge tile to push location signified by an arrow (can also click on the destination)
1. Click on the edge tile to rotate it clockwise
1. Right-click on the edge tile to rotate it anti-clockwise
1. Click the _Push Arrow_ to slide a tile piece into the grid

## Approach
This started off mostly using traditional c#, but is transitioning to use a more functional
immutable approach.

## Issues and reminders to myself
* Haven't yet turned off context-menus so I can debug more easily
* "Click to push" arrows are too small for mobile
* When dragging a tile it doesn't support the correct rotation
* css needs to be pared down. Currently more than is necessary
* Starting Blue player away from the home square so I can push it around

## Model Progress
* [x] Model the board and tiles
* [x] Rotate edge tiles
* [x] Push tiles in
* [x] Add players
* [x] Move players with tiles when they slide (including reappearing on the other side if the board)
* [ ] Game state
	* [ ] State changes on *successful* tile push
	* [ ] State changes on *successful* player move
* [ ] Each player moves on own turn
	* [ ] Current player
	* [ ] Slide
	* [ ] Move player
* [ ] Treasure cards
	* [ ] Deal the cards
	* [ ] Target
* [ ] Gameplay
* [ ] Game over

## UI Progress
* [x] Display ythe tiles on a grid
* [x] Arrows for paths
* [x] Emoji for treasures
* [x] Animate rotation of edge pieces
* [x] Add push arrows (colour indicates the player)
* [x] Display and animate sliding
* [x] Draw and display path tiles
* [x] Draw and display player pieces
* [ ] Show animation of player pieces sliding with the tiles
* [ ] Player move
	* [ ] Drag and drop
	* [ ] Click
	* [ ] Animation
* [ ] Show Treasure cards
* [ ] Game end
* [ ] Scores

