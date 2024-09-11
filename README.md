# hxrc-codetask-2024-joona-v

Play on itchio:  
https://coronal556.itch.io/2024-coding-task-joona-v  

My fork of the minigame coding task  
  
This is my version of the infinite ball jumper game.  
  
Features:  
	- procedural / endless spawning of obstacles and game objects - The algorithm is almost random but it has a few rules to prevent some stupid stuff from happening  
	- obstacles get progressively harder the higher player goes, because the obstacle spin speed increases  
	- old obstacles passed by player are destroyed below player, for optimization purposes  
  
Features I would've added if I had time:  
1. More obstacle variety - Originally i planned to create much more cooler obstacles by utilizing prefabs. However I chose to instead focus on the endless spawning algorithm which consumed most of my work time.  
(the spawner still has a feature to add multiple obstacle prefabs to the spawn pool if one wished to do so. This can be done in the ObjectSpawner inspector)  
2. Simple high-score feature. Every time user reaches new highscore, it should be stored in memory and showed to the user in the HUD.  



