# YarnSpinner Version 1 to Version 2 Migration Guide

## Overview

This document provides a comprehensive step-by-step guide for migrating the NoMansLand Unity project from YarnSpinner Version 1 to Version 2. The migration involves significant breaking changes in both the C# API and Yarn script syntax.

## Current YarnSpinner v1 Implementation Analysis

### Affected Files

#### C# Scripts Using YarnSpinner:
1. **Assets/scripts/Dialog/DialogManager.cs** - Main dialogue management system
2. **Assets/scripts/Dialog_Manager.cs** - Alternative dialogue manager (appears to be legacy)
3. **Assets/scripts/Expression_Manager.cs** - Handles character expressions and speaker management
4. **Assets/scripts/InteractionTrigger.cs** - Triggers dialogue interactions
5. **Assets/scripts/PlayerDialog.cs** - Player dialogue component (minimal implementation)

#### Yarn Script Files:
- **Assets/text/** - Contains 39 .yarn files (Molly.*, Intro1, ForestStart, etc.)

#### Package Configuration:
- **Packages/manifest.json** - Currently uses `dev.yarnspinner.unity` from GitHub

#### Unity Scene:
- **Assets/scenes/YarnSpinnerLab.unity** - Test scene for YarnSpinner functionality

## Key Breaking Changes from v1 to v2

### 1. Yarn Programs Structure Change
- **v1**: Individual .yarn files added directly to DialogueRunner
- **v2**: All .yarn files must be grouped into a single Yarn Program asset

### 2. DialogueUI to Dialogue Views System
- **v1**: Single DialogueUI component handles all presentation
- **v2**: Multiple Dialogue Views can handle different aspects (text, audio, portraits)

### 3. Variable Declaration Requirement
- **v1**: Variables could be used implicitly
- **v2**: All variables must be explicitly declared with type and default value

### 4. Command Handler Changes
- **v1**: Commands use string parameters only
- **v2**: Commands can use typed parameters and optional parameters

### 5. Yarn Script Syntax Changes
- **v1**: Uses `[[Options]]` for choices
- **v2**: Uses `->` shortcut options (planned for future betas)

## Migration Steps

### Phase 1: Package Update

#### Step 1.1: Update Package Reference
**File**: `Packages/manifest.json`

**Current**:
```json
"dev.yarnspinner.unity": "https://github.com/YarnSpinnerTool/YarnSpinner-Unity.git"
```

**Action**: Update to specific YarnSpinner v2 release:
```json
"dev.yarnspinner.unity": "https://github.com/YarnSpinnerTool/YarnSpinner-Unity.git#v2.0.0"
```

### Phase 2: Yarn Program Migration

#### Step 2.1: Create Yarn Project Asset
1. In Unity Editor, right-click in Project window
2. Select: Create → Yarn Spinner → Yarn Project
3. Name it: `NoMansLandDialogueProject`

#### Step 2.2: Configure Yarn Project
1. Select the new Yarn Project asset
2. In the Inspector, set **Source Files** to: `**/*.yarn`
3. This will automatically find all .yarn files in subdirectories
4. Verify all 39 .yarn files from `Assets/text/` are detected

#### Step 2.3: Alternative: Manual Script Addition
If automatic discovery doesn't work, manually add the source directory:
1. Set **Source Files** to: `Assets/text/**/*.yarn`
2. Or create the Yarn Project inside the `Assets/text/` folder

### Phase 3: C# Code Migration

#### Step 3.1: Update DialogManager.cs

**File**: `Assets/scripts/Dialog/DialogManager.cs`

**Key Changes Needed**:

1. **Update YarnProject Reference**:
   ```csharp
   // OLD (v1):
   [SerializeField]
   protected YarnProgram targetDialog;
   
   // NEW (v2):
   [SerializeField]
   protected YarnProject yarnProject;
   ```

2. **Update Dialogue Runner Setup**:
   ```csharp
   // OLD (v1):
   dialogueRunner.Add(targetDialog);
   
   // NEW (v2):
   dialogueRunner.SetProject(yarnProject);
   ```

3. **Update Command Handler Registration**:
   ```csharp
   // OLD (v1):
   dialogueRunner.AddCommandHandler("PlayInteractionSound", PlayInteractionSound);
   
   // NEW (v2):
   dialogueRunner.AddCommandHandler("PlayInteractionSound", PlayInteractionSound);
   // (Same method, but ensure parameters are typed if needed)
   ```

4. **Import Statements**:
   ```csharp
   // Add this for new attribute locations:
   using Yarn.Unity.Attributes;
   ```

#### Step 3.2: Update Dialog_Manager.cs

**File**: `Assets/scripts/Dialog_Manager.cs`

**Same changes as DialogManager.cs**:
1. Replace `YarnProgram targetDialog` with `YarnProgram yarnProgram`
2. Update `dialogueRunner.Add(targetDialog)` to `dialogueRunner.SetProgram(yarnProgram)`
3. Add `using Yarn.Unity.Attributes;`

#### Step 3.3: Update Expression_Manager.cs

**File**: `Assets/scripts/Expression_Manager.cs`

**Changes Needed**:
1. Add `using Yarn.Unity.Attributes;`
2. Command handlers remain the same syntax but ensure proper parameter typing:
   ```csharp
   // Consider updating to typed parameters:
   public void SetExpression(string eyesState, string mouthState = "idle")
   {
       var speakerEyesState = string.IsNullOrEmpty(eyesState) ? "idle" : eyesState;
       var speakerMouthState = string.IsNullOrEmpty(mouthState) ? "idle" : mouthState;
       expressionAnimationManager.ChangeExpression(speakerEyesState, speakerMouthState);
   }
   ```

#### Step 3.4: Update InteractionTrigger.cs

**File**: `Assets/scripts/InteractionTrigger.cs`

**Changes Needed**:
1. No direct YarnSpinner code changes needed
2. Ensure it references the updated DialogManager correctly

#### Step 3.5: Update PlayerDialog.cs

**File**: `Assets/scripts/PlayerDialog.cs`

**Changes Needed**:
1. Update field reference:
   ```csharp
   // OLD:
   [SerializeField] YarnProgram yarnDialog;
   
   // NEW:
   [SerializeField] YarnProgram yarnProgram;
   ```

### Phase 4: Yarn Script Syntax Updates

#### Step 4.1: Variable Declaration

All Yarn scripts must declare variables before use. Review each .yarn file and add declarations:

**Example for Molly.BrokenPool.yarn**:
```yarn
// Add at the beginning of the file
<<declare $demo_played = 0>>

title: Demo
tags: 
position: 115,-198
---
<<if $demo_played is null>>
<<SetSpeaker Molly idle smile>>
Ok, so here's my day so far.
<<SetExpression idle annoyed>>
Meena chased me home after school today.
<<SetExpression surprise surprise>>
I had to flee for my life!
<<SetExpression surprise frown>>
I was going so fast, I didn't see the magic hole in the sidewalk.
<<SetSpeaker Molly idle smirk>>
Dumb mistake, right?
<<SetExpression surprise annoyed>>
Well, I fell in and landed in some kind of park, I think?
<<SetSpeaker Molly idle annoyed>>
Inside I found a dead guy, who I guess used to be a king or something?
<<SetExpression surprise annoyed>>
At first, I thought he was scary, but then he gave me a magic stick.
<<SetSpeaker Molly idle smile>>
And now I can summon magical animals.
<<SetSpeaker Molly idle smirk>>
Well, one magical animal. 
A frog.
I hope he's stronger than he looks.
<<SetExpression surprise smile>>
So technically, I'm a wizard now?
<<SetExpression surprise annoyed>>
Or is it a witch?
I dunno.
<<SetSpeaker Molly idle smile>>
Make sense?
<<SetSpeaker Molly idle smirk>>
Yeah, I don't get it either.
<<SetExpression surprise frown>>
I just want to go home!
<<SetSpeaker Molly idle frown>>
But apparently, that's not an option.
So I'm stuck here looking for answers...
...with only a memory-challlenged ghost to talk to.
<<SetSpeakerName King's-Ghost>>
I beg your pardon!
<<SetSpeakerName Molly>>
<<SetSpeaker Molly idle annoyed>>
Hey, I'm not the one here who can't remember my own name.
<<SetSpeaker Molly idle smirk>>
No offense.
<<SetSpeakerName King's-Ghost>>
Hmph.
<<SetSpeakerName Molly>>
<<SetSpeaker Molly idle smirk>>
We've got to think of something better to call you than "King's Ghost".
<<SetSpeakerName King's-Ghost>>
That may be so, but let us please focus on the task at hand!
<<SetSpeakerName Molly>>
Geez, you talk funny.
<<SetSpeaker Molly idle annoyed>>
Fine.
<<set $demo_played to 1>>
<<SetSpeaker Molly idle smile>>
Let's look around for these "unfamiliar" things then.
I hear some strange noises coming from the east...
<<endif>>
```

#### Step 4.2: Command Parameter Updates

Ensure all custom commands have properly typed parameters. Current commands in use:
- `PlayInteractionSound`
- `SetSpeakerName`
- `TriggerEndTutorial`
- `TriggerEndScene`
- `SetSpeaker`
- `SetExpression`
- `switchExpression`
- `walk1`, `crouch`, `run1` (animation commands)

### Phase 5: Unity Scene Updates

#### Step 5.1: Update DialogueRunner Configuration

**Scene**: `Assets/scenes/YarnSpinnerLab.unity` and any other scenes using DialogueRunner

1. Find DialogueRunner components in scenes
2. Replace multiple YarnProgram references with single Yarn Program asset
3. Ensure Dialogue Views are properly configured

#### Step 5.2: Configure Dialogue Views

1. Add appropriate Dialogue View components:
   - Basic DialogueUI for text display
   - Custom views for portraits if needed
2. Configure each view to handle different aspects of dialogue presentation

### Phase 6: Testing and Validation

#### Step 6.1: Compile and Test
1. Update all scripts
2. Fix any compilation errors
3. Test dialogue system in YarnSpinnerLab scene
4. Verify all dialogue triggers work correctly

#### Step 6.2: Validate Yarn Scripts
1. Check all .yarn files for syntax errors
2. Ensure variable declarations are present
3. Test command execution
4. Verify dialogue flow works as expected

## Detailed File-by-File Migration Instructions

### DialogManager.cs Migration

**Before Migration** (Key sections):
```csharp
using Yarn.Unity;

public class DialogManager : MonoBehaviour
{
    [SerializeField]
    protected YarnProgram targetDialog;
    
    protected DialogueRunner dialogueRunner;
    protected DialogueUI dialogueUI;
    
    void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
        dialogueRunner.Add(targetDialog);
        // ... rest of setup
    }
    
    public void BeginDialog()
    {
        dialogueRunner.startNode = targetText;
        dialogueRunner.StartDialogue(targetText);
        // ... rest of method
    }
}
```

**After Migration**:
```csharp
using Yarn.Unity;
using Yarn.Unity.Attributes;

public class DialogManager : MonoBehaviour
{
    [SerializeField]
    protected YarnProject yarnProject;
    
    protected DialogueRunner dialogueRunner;
    protected DialogueUI dialogueUI;
    
    void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
        dialogueRunner.SetProject(yarnProject);
        // ... rest of setup
    }
    
    public void BeginDialog()
    {
        dialogueRunner.StartDialogue(targetText);
        // ... rest of method
    }
}
```

### Expression_Manager.cs Migration

**Command Handler Updates**:
```csharp
// Consider updating to use attributes instead of manual registration:
[YarnCommand("SetExpression")]
public void SetExpression(string eyesState, string mouthState = "idle")
{
    var speakerEyesState = string.IsNullOrEmpty(eyesState) ? "idle" : eyesState;
    var speakerMouthState = string.IsNullOrEmpty(mouthState) ? "idle" : mouthState;
    expressionAnimationManager.ChangeExpression(speakerEyesState, speakerMouthState);
}

[YarnCommand("SetSpeaker")]
public void UpdateSpeaker(string speakerName, string eyesState = "idle", string mouthState = "idle")
{
    if (string.IsNullOrEmpty(speakerName))
    {
        SetSpeakerName(null);
        return;
    }
    SetSpeakerName(speakerName);
    expressionAnimationManager.ChangeExpression(eyesState, mouthState);
}
```

## Post-Migration Checklist

### Code Validation:
- [ ] All C# scripts compile without errors
- [ ] YarnProgram asset is properly configured
- [ ] All .yarn files are included in the Yarn Program
- [ ] Command handlers are correctly registered
- [ ] Variable declarations exist in all .yarn files

### Runtime Testing:
- [ ] Dialogue starts correctly
- [ ] Text displays properly
- [ ] Character portraits switch correctly
- [ ] Sound effects play on interaction
- [ ] Scene transitions work as expected
- [ ] Player movement is disabled/enabled during dialogue

### Asset Verification:
- [ ] Yarn Program asset contains all dialogue files
- [ ] DialogueRunner references correct Yarn Program
- [ ] Dialogue Views are properly configured
- [ ] All scene references are intact

## Common Issues and Solutions

### Issue 1: "YarnProgram not found" Error
**Solution**: Ensure the Yarn Program asset is created and all .yarn files are added to it before assigning to DialogueRunner.

### Issue 2: Command Handler Not Working
**Solution**: Verify command handler registration happens in Awake() or Start(), and method signatures match expected parameters.

### Issue 3: Variables Not Recognized
**Solution**: Add `<<declare $variable_name = default_value>>` at the beginning of .yarn files or declare variables in Yarn Program inspector.

### Issue 4: Dialogue Not Advancing
**Solution**: Check that Dialogue Views are properly configured and MarkLineComplete() is being called when appropriate.

## Timeline Estimate

- **Phase 1 (Package Update)**: 30 minutes
- **Phase 2 (Yarn Program Setup)**: 1-2 hours
- **Phase 3 (C# Code Updates)**: 2-3 hours
- **Phase 4 (Yarn Script Updates)**: 3-4 hours
- **Phase 5 (Scene Updates)**: 1-2 hours
- **Phase 6 (Testing)**: 2-3 hours

**Total Estimated Time**: 9.5-14.5 hours

## References

- [YarnSpinner 2.0 Beta 1 Release Notes](https://github.com/YarnSpinnerTool/YarnSpinner-Unity/releases/tag/v2.0.0-beta1)
- [YarnSpinner Documentation](https://docs.yarnspinner.dev/)
- [YarnSpinner API Reference](https://yarnspinner.dev/api/beta)

## Notes

This migration guide is based on YarnSpinner 2.0 Beta 1. Future beta versions may introduce additional changes. Always test thoroughly after migration and backup your project before beginning the migration process.
