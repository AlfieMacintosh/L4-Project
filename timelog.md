# Timelog

* Alleviating Partial Eyesight Loss using a VR Headset
* Alfie Macintosh
* 2557035m
* Jan Paul Siebert

### Guidance

* This file contains the time log for your project. It will be submitted along with your final dissertation.
* **YOU MUST KEEP THIS UP TO DATE AND UNDER VERSION CONTROL.**
* This timelog should be filled out honestly, regularly (daily) and accurately. It is for *your* benefit.
* Follow the structure provided, grouping time by weeks.  Quantise time to the half hour.
___
### Week 1 : 18th Sept. - 24th Sept. - 2023

#### 21st Thursday

* *2 hours* Project meeting for Level 4


#### 22nd Friday

* *1 hour* First meeting with supervisor

___

### Week 2 : 25th Sept. - 1st Sept. : 2023

#### 28th Thursday

* *1 hour* Meeting with Dr Siebert, and fellow students under the supervision of Dr Siebert doing projects within the same field.
* *1 hour* Setting up Repository, filling in Timelog and setting out intial plan
___
### Week 3 : 2nd Oct. - 8th Oct. : 2023

#### 4th Wednesday

* *0.5 hour* Configuring Visual Studio to git repository. Pushing example template to repo.

#### 5th Thursday
* *0.5 hour* Setting up Zotero and connecting Overleaf to GitHub repo.
* *1 hour* Reading and then summarising past disertation of similar project (H. Russell, 2023)

#### 6th Friday
* *3 hours* Three hour group meeting with supervisor as well as fellow students. Primary takeaway was to get past work of students to run as intended.

___
### Week 4 : 9th Oct. - 15th Oct. : 2023

#### 12th Thursday

* *1.5 hour* Meeting with Harvey Russell, a previous student of Dr Siebert (supevisor). We talked about his work and the basic workings as well as discussing possible future works. He also pointed me in the direction of some VR tutorials online using unity.

#### 13th Friday
*  *3 hours* Supervisor meeting today 2pm.

___
### Week 5 : 16th Oct. - 22th Oct. : 2023

#### 18th Tuesday
* *5 hour* Spent the morning to early afternoon working on setting up unity environment for a VR project. Looked into possible SDKs to facilitate functional requirements.

#### 19th Wednesday
* *3 hours* Unity setup. Environment now functions in VR headset using openXR (inbuilt unity  VR package)

#### 20th Friday
*  *2 hours* Meeting, looked into other project ideas such as facilitating a test environment, then use those results to refactor users POV in vr.


___
## Week 6 : 23rd Oct. - 29th Oct. : 2023

#### 23rd Monday

* *2 hour* Experimenting with unity scene. Attempted to make some type of menu. Also looked into some tutorials

#### 26th Thursday
* *7.5 hours* Spent a lot of the morning looking at unity tutorials, for setup and also C# scripting. I think I will try and learn as I go instead of dedicating time to learning C# as scripting might not be entirely necessary.



___
### Week 7 : 30th Oct. -  5th Nov. : 2023

#### 2nd Thursday

* *3 hours* Scene transition working! Looking into passthrough


___
### Week 8 : 6th Nov. - 12th Nov. : 2023

#### 9th Thursday

* *7 hour* Tried to implement passthrough and realised that openXR isn't going to work wih vive wave (SDK). Will have to start again handpicking assets from old project...


#### 10th Friday
*  *6 hours* Talked with supervisor about the lack of feasibility of using python in unity environment. While he wanted it to be done in python I don't think its possible after speaking with H.R. and doing research as projects in unity rely on C# (primarily). Also spent all morning trying to setup project again

#### 11th Saturday
* *5 hours* Spent the morning getting project back to where it was with new VR implementation using SteamVR / OpenVR



___
### Week 9 : 13th Nov. - 19th Nov. : 2023

#### 16th Thursday
* *3.5 hours* Spent the day trying to import SRWorks to facilitate passthrough
* *3.5 hours* Passthrough added to 'Display' scene. headset now automatically changes to passthrough when display scene clicked in menu

#### 17th Friday
* *3 hours* agreed with Paul that perhaps I should focus more on displaying the results of the test, for instance a coloured HUD showcasing user results

___
### Week 10 : 20th Nov. - 26th Nov. : 2023
#### 21st Tuesday
* *4 hours* Looked into more papers on AP testing
* *1 hour* Created a scene for testing, with a semi-sphere as the testing apparatus (prototype)

#### 22nd Wednesdsy
* *2.5 hours* More reading
* *2 hours* Created a blender model for my VR display, imported into unity as an .obj file.

#### 24th Friday
* *0.5 hours* Quick supervisor meeting. Talked about software design.

___
## Week 11 : 27th Nov. - 3rd Dec. : 2023
#### 27th Monday
* *2 hours* Background reading on AP tests. How are they conducted?  
* *4 hours* Spent the rest of today implementing a triggerInput script that detects when a trigger is pressed on the controller its attached to. Uses SteamVR.


#### 30th Thursday
* *5 hours* Initial algorithm to spawn points in testing


#### 1st Friday
* *3 hours* Supervisor group meeting. He want eyetracking but I'm not convinced it will work with current SDKs.

___
### Week 12 : 4th Dec. -  10th Dec. : 2023
#### 5th Tuesday
* *4.5 hours* Algorithm for generating stimuli now includes a radius list used to calculate angle between points. Also used to move points towards/backwards from user to test perimeter vision at distance.


#### 9th Friday
* *1.5 hours* Working on cleaning up stimGenerator script.
* *2.5 hours* Supervisor wants eyetracking still however I need to finish primary work first. Might end up in further work.

___
### Week 13 : 11th Dec. - 17th Dec. : 2023


#### 12th Tuesday
* *1 hour* Looked at status report
* *2 hours* Spent time with other two students to test my environment and inputs
* *2.5 hours* background reading on input using steamVR


#### 13th Wednesday
* *7.5 hours* bunch of errors. Assets appear to be missing. Couldn't fix and using previous versions isn't fixing the issue.


#### 14th Thursday
* *3.5 hours* Spent the morning trying to fix the error of missing assets. Turns out steamVR was not running as intended with vive headset.
* *1.5 hours* after updating steamVR I re-setup the room for use with vive headset
* *0.5 hours* found secondary external sensor so vr headset can now be used while moving around (with tether limit in mind)


#### 15th Friday
* *5.5 hours* triggerInput is not interacting with stimulus-generator script as expected. Duplicate still appearing and occassionaly scripts just aren't picked up on. Furthermore sometimes only prints picked up scripts and not unpicked up scripts?


#### 16 Saturday
* *4.5 hours* Solved issue. Should now print as expected but file output will need to be updated.

___
### Week 14 : 18th Dec. - 24th Dec. : 2023

#### 18th Monday
* *2 hours* Changing format of output file
* *2 hours* background reading

#### 20th Wednesday
* *4 hours* Formatting algorithm to generate stimuli so that it includes depth for the semi-sphere shape of testing
* *2 hours* Changed triggerInput so there is a wait between recording an input so multiple inputs don't mess with output.

#### 21st Thursday
* *2 hours* More background reading for introduction of diss.
* *4 hours* Created status report

#### 22nd Friday
* *2 hours* last minute changes for status report for next semester then emailed it to supervisor
___
### Week 15 : 25th Dec. - 31st Dec. : 2023
* Christmas break!
# The New Year 2024 - Semester 2
### Week 1 : 1st Jan. - 7th Jan. : 2024
* Back home, cannot work on project without access to VR hardware. The break is nice though.

### Week 2 : 8th Jan. - 14th Jan. : 2024
#### 9th Tuesday
* *4 hours* background reading on Humphreys vision field test (HVF)
* *2 hours* Looked into using a plane instead of a spherical object when conducting testing


#### 11th Thursday
* *3 hours* Looked into the angle between stimuli if I did a similar test to HVF.
* *1 hour* angle calculated from outer test perimetry to user camera so avg user fov is used in testing.

#### 12th Friday
* *3.5 hours* Went over status report with supervisor. Looking into producing a report with user data instead of using VR to alleviate eyesight problems and instead of using a HUD.

## Week 3 : 15th Jan. - 21st Jan. : 2024
#### 15th Monday
* *4.5 hours* Looked into 30-2 testing.
* *2.5 hours* How are HVF test results formatted (greyscale, deviation graphs, fixation loss)

#### 17th Wednesday 
* *4 hours* Started abstracting generating algorithm so it generates test dependent on a .txt file.

## Week 4 : 22nd Jan. - 28th Jan. : 2024
#### 22nd Monday
* *3.5 hours* spent additional time looking into formatting test results, how stimuli look, size etc.

#### 25th Thursday
* *4 hours* algorithm working to generate all stimuli at correct co-ord.
* *4 hours* formatted where stimuli can spawn. Also implemented an x,y-axis and central stimuli which can be used for diagnostics when formatting future tests.
#### 26th Friday
* *4 hours* abstracted test now formatted correctly. output formatted correctly to so generator and input detection should be working 
* *1.5 hours* online meeting with supervisor
* *0.5 hours* Meeting With Mathew Chalmers about ethics of project testing, checking to see if there are any avoidable risks.


## Week 5 : 29th Jan. - 4th Feb. : 2024

#### 31st Wednesday
* *8 hours* Spent the entirety of the day trying to set up eye tracking us SR_Anipal. Seems to be issues with compatibility between the openVR SDK and the eyetracking SDK.
#### 1st Thursday
* *4 hours* Morning Spent trying to resolve the issue. Issue was with admin rights for PC, eye tracking can now be calibrated succesfully using headset.
* *4 hours* Spent the afternoon messing about with the sample scenes to try and understand the scripts behind eye tracking as there are literally 30 or so scripts.
#### 2nd Friday
* *4 hours* Users gaze is now highlighted by using a 'Gaze Ray'. A line going from camera to where the are looking at until a certian specified distance.

## Week 6 : 5th Feb. - 11th Feb. : 2024
#### 7th Wednesday
* *5 hours* Implenting hitboxes for gaze detection, does not appear to be working with Physics.Raycast (why???)
* *1.5 hours* Group meeting with JPS and others, showcased progress with eyetracking and annoyance at why I cannot get it to detect my objects.

#### 9th Friday
* *5.5 hours* Beginning to lose sanity as eyetracking still won't pick up objects reliably. Relooking at source samples there may be another way to do it using the eye data directly.
* * *1 hours* JPS was shown eye tracking in real-time. Could see ray, and output of co-ords of users gaze to txt. Still won't pick up objects!!!!


## Week 7 : 12th Feb. - 18th Feb. : 2024
#### 12th Monday
* *3 hours* Physics.Raycast appears to be a lost cause. Might have something to do with the ray intercepting multiple object hitboxes, but even then the colour won't change as required of the objects. Spent 3 hours with little progress.

#### 13th Tuesday
* *3 hours* Success! Eye tracking now uses eye data directly. Stimulus changes colour with new seperate script that is called when user looks at object. Also I now use mesh colliders, less efficient but more accurate.
* *1.5 hours* Central stimulus now has three possible colours. Idle orange when the environment is waiting for you to activate scripts or testing is done. Green - testing is in progress you are looking at centre, and red - you are not looking at the centre but testing is in progress.
* *1.5 hours* Testing now only begins when user clicks tracking pad with one of the controllers. 5 second delay. Eyetracking now also ceases when StimulusGenerator is done.

#### 14th Wednesday
* *8 hours* began scripting to process data

#### 15th Thursday
* *4 hours* first script is done, heatmap of user gaze done
#### 16th Friday
* *3.5 hours* worked on grayscale graphing script

#### 17th Saturday
* *2 hours* created invalid points script
* *2 hours* more work on grayscale, hopefully almost done

#### 18th Sunday
* *2 hours* grayscale looks done
* *6 hours* worked on pdf generator should be done soon

## Week 7 : 19th Feb. - 25th Feb. : 2024
#### 19th Monday
* *2 hours* pdf generator finished
* *6 hours* Worked on introduction, debrief and ethics checklist for user evaluation
* *2 hours* First user studies done

#### 21st Wednesday
* *3 hours* four more user studies done
* *3 hours* planned out background

#### 23rd Friday
* *3 hours* wrote title page and planned out subheadings, also worked on background
* *3 hours* planned out background

#### 25th Saturday
* *3 hours* A lot of reading about visual field testing
* *3 hours* A lot of reading about Virtual reality

#### 26th Sunday
* *1.5 hours* A lot less reading about visual field testing
* *1.5 hours* A lot less reading about Virtual reality

## Week 8 : 26th Feb. - 3rd Mar. : 2024
#### 26th Monday
* *8 hours* worked fully on background visual field testing. Next heading will be VR
#### 28th Wednesday
* *7 hours* finished visual field testing. Took much longer than expected and will require redraft
* *3 hours* Got fed up of visual field testing and so started working on virtual reality 
#### 29th Thursday
* *4 hours* Went back to visual field testing, finished it up with paragraph on hemianopia
* *5 hours* Now back to VR, mostly finished but chapter summary needs done
#### 2nd Saturday
* *8 hours* started analysis and requirements. Did problem statement

## Week 9 : 4th Mar. - 10th Mar. : 2024
#### 4th Monday
* *8 hours* finished requirements and problem statement and a rough summary of chapter
#### 6th Tuesday
* *1 hour* finished summary of analysis/requirements
* *1 hour* went back over background and redrafted
* *3 hours* Spent rest of evening planning out and beginning design.
#### 7th Thursday
* *10 hours* Spent all day working on design, Know what I need to talk about now.
#### 10th Sunday
* *8 hours* Finished design. Includes nice graphs and visuals


## Week 10 : 11th Mar. - 17th Mar. : 2024
#### 11th Monday
* *10 hours* Started implementation, got idea of subheadings and begin introduction and SDK paragraphsa
#### 13th Wednesday
* *5 hours* Bug with reports 'valid points' metrics spent time working on that. Also worked on implementation again
* *5 hours* Spent a long time trying to fix the reports, seems to be good now but made a custom script so I could do them automatically.
#### 14th Thursday
* *6 hours* finished up work on file structure, made a visual of scene layout
* *4 hours* working through implementation, did some nice graphs for work flow.
#### 16th Saturday
* *8 hours* finished implementation. proof read focusing on evaluation tomorrow.
#### 17th Sunday
* *4 hours* Didn't actually finish implementation, worked again on it, referencing SDKs also
* *7 hours* rest of the evening spent on laying out evaluation and starting on introduction and method

## Week 11 : 18th Mar. - 24th Mar. : 2024
#### 18th Monday
* *8 hours* Spent all day working on evaluation. Currently finished methedology
#### 19th Tuesday
* *8 hours* initial draft of abstract and finishing off methedology, also worked on further work
#### 20th Wednesday
* *2 hours* user evaluation with Paul
#### 21nd Tuesday
* *8 hours* spent time writing up user evalation results. focused on limitations and t-test
#### 22nd Tuesday
* *8 hours* t-test was a success, writing up pauls results and conclusion

## Week 12 : 25th Mar. - 29th Mar. : 2024
#### 26th Tuesday
* *6 hours* Trying to make final project an executable. Major issues with SteamVR interaction when doing so as interaction logs are stored locally outwith the executable file path, so it wouldnt be carried on. Currently as it stands it is run through unity. Benefit of this is the tester can see users progress and output directorty of report.
* *2 hours* Updating file structure. Includes updating readme's
* *2 hours* Bunch of admin I need to do for project like commenting, more readme's and still trying to solve interaction issue from earlier.

#### 27th Wednesday
* *8 hours* spent time formatting project dissertation, and redrafting diss again.

#### 28th Thursday
* *8 hours* did my presentation with slides and audio

#### 29th Friday
* *3 hours* formatting final bit of time log, repository and files to upload project


