# FruitAndVegetableRecognition
Computer Vision using Azure Cognitive Services (Custom Vision)

This site is used to train, build and deploy the custom model [Azure Services (Custom Vision)](https://www.customvision.ai/).

![Model example 1](https://ibb.co/kx6WHj6)
![Model example 2](https://ibb.co/qnnJS6n)

## RUN
Open FruitVegetableML.sln using Visual Studio, F5 to build and run


## SCENARIO
<a href="https://ibb.co/NSGTzh9"><img src="https://i.ibb.co/gVc4C1S/Picture3.png" alt="Picture3" border="0"></a>

- Camera will only start and detect object when an item is placed on the weighing scale.
- WeighingScaleStatus is to mimic the status of the scale. (0 no item is placed, 1 item is placed)

1. Item placed on weighing scale
2. Camera starts up and capture the image
3. Saving the captured image into desired folder*
4. Machine learning (item detection and prediction starts)
5. Display the probability of the image to the different number of categories.
6. System should only display the category with the highest probability. (NOT DONE YET)


*Images captured and saved in the folder can be used for further training.

