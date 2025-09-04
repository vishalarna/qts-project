import { moduleMetadata } from '@storybook/angular';
import { CommonModule } from '@angular/common';
// also exported from '@storybook/angular' if you can deal with breaking changes in 6.1
import { Story, Meta } from '@storybook/angular/types-6-0';

import { CardComponent } from './card.component';

import { IconComponent } from '../../base/icon/icon.component';
import { ButtonComponent } from '../../base/button/button.component';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatButtonModule } from '@angular/material/button';
import 'leader-line';

export default {
  title: 'Training Map/Card',
  component: CardComponent,
  decorators: [
    moduleMetadata({
      declarations: [IconComponent, ButtonComponent],
      imports: [MatIconModule,MatCardModule, DragDropModule, MatButtonModule, MatIconModule],
    }),
  ],
  argTypes: { addNew: { action: 'addNewClicked' } },
} as Meta;

const Template: Story<CardComponent> = (args: CardComponent) => ({
  props: args,
});

export const Default = Template.bind({});
Default.args = {
  nodes : [
    {
      title: 'Position',
      iconName: 'business_center',
      id: 'first_node',
      text: 'First Node',
      nodeType: 'simple'
    },          
  ],
  //Category: '',
};
