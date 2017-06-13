#PlayMaker uGui Change log

###1.3.2
**Release** 09/01/2017  

**fix** 
- Reuse EventData Static, instead of creating new to avoid loosing data during actions stack processing  

**udpate**  
- PlayMaker Utils to latest

###1.3.1
**Release** 09/01/2017  

**new**  
- new action `uGuiGraphicCrossFadeColor`  
- new action `uGuiGraphicCrossFadeAlpha`  

###1.3
**Release** 25/10/2016  

**udpate**  
- updated GetLastPointerData to inform about Inputbutton  

###1.2.1
**Release** 15/09/2016  

**new**  
- new action `uGuiInputFieldGetTextAsFloat`  
- new action `uGuiInputFieldGetTextAsInt`  

###1.2.0
**Release** 27/06/2016  

**new**
- Dropdown support on 5.2+  


###1.1.9
**new** 
- new action `EventSystemExecuteEvent` 
- new action `EventSystemCurrentRayCastAll` 
- new sample `uGuiVrGazeButton`

**udpate**
- PlayMakerUtils  


###1.1.8
**Fix** fixed obsolete action `uGuiInputFieldScreenToLocal` for Unity 5.3+

###1.1.7
**Fix** Fixed `uGuiImageGetSprite`

###1.1.6
**Fix** Fixed `uGuiToggleGetIsOn`

###1.1.5
**new**
- Fixed `radialLayout` plublishing issues with missing onValidate() overide  
- Fixed 4.7 compile flags within PlayMaker Utils  


###1.1.4
**fix**
- Fixed `uGuiToggleSetIsOn` bad class name  
- Fixed api upgrades on 5.X on RectTransform custom actions


###1.1.3
**new**
- new action `RectTransformContainsScreenPoint` 
- new action `RectTransformScreenPointToWorldPointInRectangle`
- new action `RectTransformScreenPointToLocalPointInRectangle`
- Fixed obsolete properties in `GetLastPointerEventData` action

###1.1.2
**New**  
- new action `uGuiInputFieldOnSubmitEvent` 
- new action `uGuiInputFieldGetWasCanceled`
- new action `uGuiInputFieldGetIsFocused`
- improved `uGuiInputFieldOnEndEditEvent` with text access and wasCanceled property access
- improved `PlayMakerUGuiComponentProxy` for InputField EndEdit Event to pass wasCanceled in the EventData.
 
**Fix**  
- removed unecessary RequiredField attribute in action `uGuiInputFieldGetIsFocused` and `uGuiInputFieldGetHideMobileInput`  

 
###1.1.1
**New**  
- new get/set local position and rotation for RectTransform  
- new Drop proxy
- new sample Drag and Drop


**Fix**  
- Fixed `IsPointerOverUiObject` action
- Fixed Ecosystem drag example to use the new actions

**Improvement**  
- publishing alternative package with all ugui actions 
- renamed few actions to keep consistency  
- added sentByGameObject for Proxy events

###1.1.0

**Improvement**  
- Comply with Ecosystem versioning and changelog  
- better Git rep structure with PlayMaker Utils as SubModule  

**New**  
- Added EventSystem support for drag and raw pointer events  


###1.0.0

**New:**  
- Initial release  

