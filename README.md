# GWDNotAGameStudio

Game World Design project

---

To clone the repository: git clone --recurse-submodules https://github.com/Jazzpah01/GWDNotAGameStudio.git

To update submodules: git submodule foreach git pull origin main

If you already have clones the project, run the following:

git submodule init

git submodule update

----

Features:

-------------------------
Unity version used: Unity 2021.2.13f1
Can be downloaded here: https://unity3d.com/get-unity/download/archive

Don't use other versions of 2021.2, because we can't downgrade.

-------------------------
GIT INFORMATION:
The main branch is used only when we are making official builds. When the code has been tested and is ready.
The develop branch is where we push the features we make.

Whenever you are developing on a feature, create a branch from develop called feature/x (where x is what you are trying to make). 
Only when the feature is fully implemented will you do a pull request to develop. Example: If I want to create a movement feature, 
I will create a new branch which is a copy of develop and call it feature/movement. When I am finished making this, I will make a 
pull request and merge develop and feature/movement.

Prototype branches will be prototype/x, where x is the number of the prototype.

DON'T CHANGE THE MAIN SCENE. Everyone can create a personal folder called Personal inside the Scenes folder. Here you can create 
scenes that only you will be able to access. Only tech lead will be changing the main scene.

Any folder called personal or Personal will be ignored by git. Use folders with these names only for files you don't want to push
to git. (i.e. test files)

-------------------------
How to get a remote branch NAME to your computer.

git fetch origin

git checkout -b NAME origin/NAME

---
How to upload a branch NAME that is only local.

git push -u origin NAME

---
How to git add everything in a folder

git add FOLDER

Example: git add Assets/Models/

---
To list remove branches: git branch -v -a

https://stackoverflow.com/questions/1783405/how-do-i-check-out-a-remote-git-branch

-------------------------
Git cheat-sheet: https://www.atlassian.com/git/tutorials/atlassian-git-cheatsheet
