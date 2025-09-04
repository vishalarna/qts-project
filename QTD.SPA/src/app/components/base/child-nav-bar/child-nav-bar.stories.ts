import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { MatIconModule } from "@angular/material/icon";
import { RouterTestingModule } from "@angular/router/testing";
import { Meta, moduleMetadata, Story } from "@storybook/angular";
import { AddNewMenuItemComponent } from "../add-new-menu-item/add-new-menu-item.component";
import { ButtonComponent } from "../button/button.component";
import { IconComponent } from "../icon/icon.component";
import { LabelComponent } from "../label/label.component";
import { LinkComponent } from "../link/link.component";
import { TextboxComponent } from "../textbox/textbox.component";
import { ChildNavBarComponent } from "./child-nav-bar.component";

export default {
  title: 'Base Components/MenuItem',
  component: ChildNavBarComponent,
  decorators: [
    moduleMetadata({
      declarations: [
        LinkComponent,
        LabelComponent,
        IconComponent,
        ButtonComponent,
        TextboxComponent,
        AddNewMenuItemComponent,
      ],
      imports: [CommonModule, FormsModule, MatIconModule, RouterTestingModule],
    }),
  ],
  argTypes: { ItemSaved: { action: 'Item Saved' } },
} as Meta;

const Template: Story<ChildNavBarComponent> = (args: ChildNavBarComponent) => ({
  props: args,
});

export const ChildMenuWithChildren = Template.bind({});
ChildMenuWithChildren.args = {
  title: '',
  Data: [
    {
      title: 'Class Schedule',
      routePath: '/ClassSchedule'
    },
    {
      title: 'Certification Expiration',
      routePath: '/CertificationExpiration'
    },
    {
      title: 'EMP Login',
      routePath: '/EMPLogin'
    },
    {
      title: 'EMP Test',
      routePath: '/EMPTest'
    },
    {
      title: 'EMP Pretest',
      routePath: '/EMPPretest'
    },
    {
      title: 'EMP Course',
      routePath: '/EMPCourse'
    },
    {
      title: 'EMP Student Evaluation',
      routePath: '/EMPStudentEvaluation'
    },
    {
      title: 'EMP Procedure Review',
      routePath: '/EMPProcedureReview'
    },
    {
      title: 'EMP IDP Review',
      routePath: '/EMPIDPReview'
    },
    {
      title: 'EMP Task Qualification - Trainee',
      routePath: '/EMPTasKQualificationTrainee'
    },
    {
      title: 'EMP Task Qualification - Evaluator',
      routePath: '/EmpTaskQualificationEvaluator'
    },
    {
      title: 'EMP Self-Registeration Approval',
      routePath: '/EmpSelfRegisterationApproval'
    },
    {
      title: 'EMP Self-Registeration Denial',
      routePath: '/EMPSelfRegisterationDenial'
    },
    {
      title: 'EMP DIF Survey',
      routePath: '/EMPDIFSurvey'
    },
    {
      title: 'EMP GAP Survey',
      routePath: '/EMPgapSurvey'
    }
  ]
};
