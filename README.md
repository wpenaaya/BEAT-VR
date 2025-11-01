# Overview
The project has a main component which is the “DrumGame”. This is at the top of the hierarchy, along with the scene “Venue_Bar”. The scene can be changed out easily and “DrumGame” can be turned into a prefab if needed. As children of this “DrumGame” object, we have things that are necessary no matter the scene or game state. These are the stage, the menu, the main sound source, the drums, and the objects with scripts that control post processing and lighting effects.

The main game is controlled through a couple of scripts which do most of the heavy lifting in terms of actual gameplay. These are in the DrumGame game object, and hold references to important things such as the drums out there, the note spawn positions, etc. There’s scripts for playing songs, scripts to judging hits, scripts to keep score.

The interactive elements are controlled by their own scripts as well. The Menu is controlled by an Event System with a hacky script that lets it accept input from SteamVR Actions. The drums and the drumsticks interact through a common interface and the drums are derived from an abstract drum class so different kinds of percussion instruments can be added, whether they be cymbals or cowbells or triangles. Drum hits are also implemented through C# events to keep them snappy.

Lastly there’s Menu scripts and visual effects scripts. The menu, effects, and general game state scripts use unity events to make it easier to extend on them.

# Scripts
-   Assets/Scripts/
    -	FXScript
        -	This script takes in several float parameters to control the intensity of in game visual effects.
        -	The two effects this script controls are a pulsing effect when you hit a drum while playing a map, and the fog that goes away and comes back when you start and stop a map (or more specifically when you close and open the menu).
        -	It uses a coroutine call with WaitForSeconds() which interpolates post processing and lightings settings over time, and uses an easeInOutCubic interpolation function from easings.net to produce a smoothed lerp value.
    -	ResetView
        -	This simple script is attached to the CameraRig, and simply resets your camera position (head position) to the default position (ignoring the y component, and only using y-rotation) with the press of the touchpad.
    -	DrumsAndHitters/
            -	AbstractDrumHitter
                -	This is a script that can be attached to any object with a collision box.
                -	Takes in a couple of floats for haptics intensity and how hard you have to hit the drum with the stick.
                -	It takes a reference to a transform, calculates its speed, and uses that speed to calculate the hit volume through a lerp.
                -	It can be to hit any object that implements the Hittable interface.
                -	It has an abstract Hit() method so the developer can specify if this collider is supposed to produce a direct hit sound, or a stick hit sound.
        -	AttachDrumStick
            -	Not used
        -	DrumScript
            -	Implements the Hittable interface, allowing it to be hit by a concrete DrumHitter.
            -	Takes in the sounds it makes on direct and stick hits, a score popup prefab (a textmesh object), a transform out of which to spawn the score popups, some floats for setting haptics parameters (allowing every drum to “feel” differently when hit), and an enum to say what type of a drum hit it was (what track it corresponds to in a song Map).
            -	Uses a C# event with a Func<> delegate to reduce overhead and latency. Once the event returns a HitScore, it displays it as a popup.
            -	Requires an AudioSource component to be on the same object its attached to.
        -	HittableInterface
            -	Defines an interface for an object that can be hit.
        -  	NoteTrack
            -  	Simply keeps references to the places where notes spawn and where they end.
        -	ScorePopupScript
            -	Attached to the score popup GameObject that DrumScript references.
            -	During a score popup’s lifetime, it floats upwards and fades away using some lerp functions, where the lerpValue is eased In.
            -	Uses an easeInCubic function from easings.net.
        -	ScoreSystem
            -	Takes in numerical values for how much each type of HitScore is worth. 
            -	Keeps track of score, also keeps track of misses.
            -	Can easily be extended to include combos.
        -	StickScript
            -	Concrete class of AbstractDrumHitter. Calls StickHit on the object hit.
        -	TipScript
            -	Concrete class of AbstractDrumHitter. Calls TipHit on the object hit.
    -	GameScripts/
        -	DrumGame
            -	The main class of the game.
            -	Takes in references to every drum, a main audioSource, a NoteCreator, a ScoreSystem, and has some UnityEvents to control visual effects and inform the menu that it needs to either close or open back up. Also has some UnityEvents that update UI elements.
            -	Registers event listeners for each drum.
            -	Sets up and tears down the necessary things to play a song: a MapPlayer with a Map, a noteCreator, a HitJudge, a ScoreSystem.
            -	Manages hit events. It judges the hit, feeds the HitScore to the ScoreSystem, Updates UI, then returns the HitScore. 
        -	DrumInputs
            -	An enum of allowed drum inputs. These correspond to the amount of tracks in a Map.
        -	HitJudge
            -	Judges timing of the hit against the nearest song in the Map’s corresponding track, returns a HitScore.
            -	Uses a binary search for speed. 
        -	HitScores
            -	An enum of all HitScores. Has a “NONE” HitScore as well for when the game isn’t playing.
        -	Map
            -	Holds all of the information pertaining to a Map, such as the tempo, length, audio file, start offset, the notes. Also initializes these. 
        -	MapPlayer
            -	A heavy lifter. It actually plays a Map. This is the main rhythm game component.
            -	Notifies, through a UnityEvent, the DrumGame when the song stops.
            -	Thanks to the knowledge in an article acknowledged in attributions.txt, I use the audio system’s time to sync everything up, instead of a system time or delta time.
        -	NoteCreator
            -	Holds references to tracks and notes, and spawns notes according to the track they belong to, and sets up their variables (like end position and duration).
            -	Holds all the notes that have been spawned, in case they’re needed later (this functionality was later scrapped).
        -	NoteScript
            -	Simply interpolates between a spawn position and an end position according to a duration and the time elapsed.
    -	Options/
        -	AdjustDrums
            -	Exposes some methods that can be called by a UnityEvent to move the drums around.
        -	CustomInput
            -	Builds off of the StandaloneInputModule and calls some events to translate SteamVR_Actions into UI inputs.
    -	UI/
        -	Menu
            -	Handles what to do when closing and opening the menu, and closing the application.
            -	Tells the FX Script to perform fog-related methods too.
Imported Assets
Pretty much all of the assets that I’ve imported from the Asset Store have been for cosmetics. Things like chairs, textures, paintings, tables, nothing more. I’ve imported:
-	Frames with pictures
-	Bar props
-	Bottles
-	Some furniture
-	Some concert-related prefabs
-	Wood textures
Other than that, I’ve also gotten some fonts off of 1001fonts.com, and some sound effects off of freesound.org. I’ve made all of the songs in this project, as well as the model for the drumsticks.
Additional Information
I’ve spend most of my time trying to get the interaction between the drums and the sticks to “feel” natural, responsive, and convincing. However, after trying every single kind of joint, failing at writing a quaternion-based spring joint, and everything in between, I decided to simply make the sticks kinematic. Beforehand, my idea was to have some kind of springy joint let the sticks react to the momentum of the hand, and the hit of the drums. Although it worked great in best-case scenarios, it glitched out way too much.
If you wish to try my last attempt at getting this to work (using two nested hinge joints), then look at the Drumsticks prefab (just Drumsticks, not Drumsticks old). Place X rotation as a child of Z rotation, and the Drumsticks as the child of X rotation. Set up the hinges so that they’re attached to the children below. The springiness has already been calibrated here.  Note here that there is no momentum when swinging the drumsticks in this setup, as X rotation is kinematic by default, so in their local space they’re not moving much at all until the drumsticks actually hit something. To make it not so, attach the Z rotation to a hinge in the DrumStickTarget GameObject, which is a child of the SteamVR CameraRig’s hands. If you do that, then whenever you move your hands, the sticks will have their own angular momentum that feels springy, and lets you do drum hits in a more natural way… if it doesn’t completely glitch out and hit the drumhead 5 times in one second.

# BEAT VR User Guide
Running this program is rather simple.
Download the Unity project and open the only scene in Assets/Scene. Once there, configure your inputs through Window > VR Input and ensure that you have the following actions bound for the “drumGame” actionset:
-	MenuUp
-	MenuDown
-	MenuLeft
-	MenuRight
-	Menu
-	CameraReset
The other actions are not necessary.
This project was intended to be used with Valve Knuckles controllers, since they way that they’re made allow you to hold onto real life drumsticks as you play! If you have Valve Knuckles, grab your favourite drumsticks, and set up 4 practice pads next to each other in front of you. Then, when you start up the game, adjust the height of the drums in game. Reset the camera position in an optimal position, and get playing!
The first track, ClickTrack, is simply a click track playing at 120 beats per minute. It’s a test track to calibrate your setup.  The second track, Track1, is a song I made. It’s the only “real” song in the game but it should give you an idea of how the game could work.
When the songs are done, you’ll be taken back to the main menu. From there, you can Quit.
