   M S B A M L   `   `   `  ����F  BZooScenario, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ZooScenarioZooScenario  5   	       ZooScenario.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:ZooScenario     5   	   -   5          �Name$  window��5   	    � Title$( "Object-Oriented Programming 2: Zoo��5   	   $	��330��64   $	��752��6A    � WindowStartupLocation$ CenterScreen=�6M   .��5	      � $��0,0,10,0q�6   .+�5
   
   �� -   5         ��Name$ admitGuestButton��5
      $��Admit Guest��6,   $
��Left=�6B   $	��Top=�6]   $��	10,10,0,0q�6u   $	��230��6�   6�   �� 5   
   -   5        $ feedAnimalButton��5      $��Feed Animal��6+   $
��Left=�6A   $	��Top=�6\   $��	10,40,0,0q�6t   $	��230��6�   6�   �� 5   
   -   5         ��Name$ increaseTemperatureButton��5      $��2��6<   $��+��6L   $��Right=�6X   $��	0,0,40,50q�6t   $��Bottom=�6�   $��23��6�    J� RenderTransformOrigin$ 2.895,0.553&�6�   $��23��6�   6�   �� 5   
   -   5        $ decreaseTemperatureButton��5      $��2��6<   $��-��6L   $��Right=�6X   $��	0,0,40,20q�6t   $��Bottom=�6�   $��23��6�   $ 2.895,0.553&�6�   $��23��6�   6�   �� 5   
   -   5         ��Name$ temperatureBorder��5      
���   �$��1q�6@   $��2��6T   $��Right=�6d   $	��170��6�   $��0,187,10,20q�6�   $��Bottom=�6�   $��20��6�   
���  ��6�   �� 5   
   -   5         ��Name$ temperatureLabel��5      $��2��6*   $
��Temp��6:   $��Right=�6I   $��	0,0,32,75q�6e   $��Bottom=�6x   �� 5   
   -   5         ��Name$ animalListBox��5      $��Right=�6)   $��
0,0,300,10q�6E   $��Bottom=�6Y   $	��180��6t   $	��270��6�   6�   �� 5   
   -	   5        $ guestListBox��5      $��Right=�6(   $��
0,0,100,10q�6D   $��Bottom=�6X   $	��180��6s   $	��270��6   6�   �� 5   
   -
   5        	 ��Name$	 animalTypeComboBox��5      $
��Left=�6/   $��	10,90,0,0q�6J   $	��Top=�6]   $	��230��6u   $��35��6�   �� 5   
   -   5        $ addAnimalButton��5      $��
Add Animal��6*   $
��Left=�6?   $��
10,137,0,0q�6Z   $	��Top=�6n   $	��110��6�   $ 0.476,0.152&�6�   6�   �� 5   
   -   5        $ removeAnimalButton��5      $��Remove Animal��6-   $
��Left=�6E   $��130,137,0,0q�6`   $	��Top=�6u   $	��110��6�   $ 0.476,0.152&�6�   6�   �� 5   
   -   5        $ removeGuestButton��5      $��Remove Guest��6,   $
��Left=�6C   $��130,169,0,0q�6^   $	��Top=�6s   $	��110��6�   $ 0.476,0.152&�6�   6�   �� 5   
   -   5        $ addGuestButton��5      $��	Add Guest��6)   $
��Left=�6=   $��
10,169,0,0q�6X   $	��Top=�6l   $	��110��6�   $ 0.476,0.152&�6�   6�   �� 5   
   -   5        $ showCageButton��5      $��	Show Cage��6)   $
��Left=�6=   $��
10,199,0,0q�6X   $	��Top=�6l   $	��110��6�   $ 0.476,0.152&�6�   6�   �� 5   
   -   5        $ adoptAnimalButton��5      $��Adopt Animal��6,   $
��Left=�6C   $��
10,230,0,0q�6^   $	��Top=�6r   $	��110��6�   $ 0.476,0.152&�6�   6�   �� 5   
   -   5        $ unadoptAnimalButton��5      $��Unadopt Animal��6.   $
��Left=�6G   $��130,230,0,0q�6b   $	��Top=�6w   $	��110��6�   $ 0.476,0.152&�6�   6�   5      5      