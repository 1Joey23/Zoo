   M S B A M L   `   `   `  ���� F  BZooScenario, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ZooScenarioZooScenario      ZooScenario.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:ZooScenario     -     �Name$  window�� � Title$( "Object-Oriented Programming 2: Zoo��$	��330��$	��752�� � WindowStartupLocation$ CenterScreen=�.��� $��0,0,10,0q�.+��� -    ��Name$ admitGuestButton��$��Admit Guest��$
��Left=�$	��Top=�$��	10,10,0,0q�$	��230���� -   $ feedAnimalButton��$��Feed Animal��$
��Left=�$	��Top=�$��	10,40,0,0q�$	��230���� -    ��Name$ increaseTemperatureButton��$��2��$��+��$��Right=�$��	0,0,40,50q�$��Bottom=�$��23�� J� RenderTransformOrigin$ 2.895,0.553&�$��23���� -   $ decreaseTemperatureButton��$��2��$��-��$��Right=�$��	0,0,40,20q�$��Bottom=�$��23��$ 2.895,0.553&�$��23���� -    ��Name$ temperatureBorder��
���   �$��1q�$��2��$��Right=�$	��170��$��0,187,10,20q�$��Bottom=�$��20��
���  ���� -    ��Name$ temperatureLabel��$��2��$
��Temp��$��Right=�$��	0,0,32,75q�$��Bottom=��� -    ��Name$ animalListBox��$��Right=�$��
0,0,300,10q�$��Bottom=�$	��180��$	��270���� -	   $ guestListBox��$��Right=�$��
0,0,100,10q�$��Bottom=�$	��180��$	��270���� -
   	 ��Name$	 animalTypeComboBox��$
��Left=�$��	10,90,0,0q�$	��Top=�$	��230��$��35���� -   $ addAnimalButton��$��
Add Animal��$
��Left=�$��
10,137,0,0q�$	��Top=�$	��110��$ 0.476,0.152&��� -   $ removeAnimalButton��$��Remove Animal��$
��Left=�$��130,137,0,0q�$	��Top=�$	��110��$ 0.476,0.152&��� -   $ removeGuestButton��$��Remove Guest��$
��Left=�$��130,169,0,0q�$	��Top=�$	��110��$ 0.476,0.152&��� -   $ addGuestButton��$��	Add Guest��$
��Left=�$��
10,169,0,0q�$	��Top=�$	��110��$ 0.476,0.152&��� -   $ showCageButton��$��	Show Cage��$
��Left=�$��
10,199,0,0q�$	��Top=�$	��110��$ 0.476,0.152&�