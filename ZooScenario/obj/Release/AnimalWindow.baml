   M S B A M L   `   `   `  ���� F  BZooScenario, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ZooScenarioZooScenario      ZooScenario.AnimalWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:ZooScenario     -     � Title$  AnimalWindow��$	��450��$	��800��.��� $��0,0,0,-6q�.+�� $��56,48,65,59q� � RowDefinitions �� $��4* ��� $	��30* ��� $��4* ��� $	��30* ��� $��4* ��� $	��30* ��� $��4* ��� $	��30* ��� $��4* ��� $	��30* � � ColumnDefinitions �� $
��100* ��� $
��400* �.+��� -    ��Name$ 	nameLabel��$��Name:��$��1��$��Right=�$��Center=�$��24(��� -   $ genderLabel��$��Gender:��$��3��$��Right=�$	��Top=�$��24(�$��0,10,0,0q��� -   $ ageLabel��$
��Age:��$��5��$��Right=�$	��Top=�$��24(� J� RenderTransformOrigin$ 0.405,3.872&�$��0,10,0,0q��� -   $ weightLabel��$��Weight:��$��7��$��Right=�$	��Top=�$��24(�$��0,10,0,0q��� -   $ pregnancyStatusLabel��$��No��$��Right=�$��10��$	��Top=�$��24(�$��0,10,0,0q��� -    ��Name$ nameTextBox��$��1��$��2��$
��Left=�$��	39,12,0,0q� �� TextWrapping$
 Wrap=�$��nameTextBox��$	��Top=�$	��405��$��39��$��28(��� -   $ 
ageTextBox��$��5��$��2��$
��Left=�$��	39,10,0,0q�$
 Wrap=�$��
ageTextBox��$	��Top=�$	��405��$��39��$��28(��� -	   $ weightTextBox��$��7��$��2��$
��Left=�$��	39,12,0,0q�$
 Wrap=�$��weightTextBox��$	��Top=�$	��405��$��39��$��28(��� -
    ��Name$ genderComboBox��$��3��$��2��$
��Left=�$��	39,10,0,0q�$	��Top=�$	��405��$��39��$��18(��� -    ��Name$ makePregnantButton��$��9��$��2��$��makePregnantButton��$
��Left=�$��	39,13,0,0q�$	��Top=�$ -2.199,0.208&�$��36��$	��405��$��24(��� -   $ okButton��$��OK��$��Right=�$��
0,0,400,20q�$��Bottom=�$��35��$	��170��$��18(�	 �� 	IsDefault	 . �� -   $ cancelButton��$��Cancel��$��Right=�$��
0,0,165,20q�$��Bottom=�$��35��$	��170��$��18(�
 �� IsCancel
 . 