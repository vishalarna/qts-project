import { Component } from "@angular/core";

@Component({
  selector:'app-preview-test',
  templateUrl: './preview-test.component.html',
  styleUrls:['./preview-test.component.scss'],
})

export class PreviewTestComponent{
  questions:any=[
    {'num':'1'
    ,'statement':"What is the color of the sky?",
    'options':['Yellow','Green','Blue','Torquoise']
  },
  {'num':'2'
    ,'statement':"What is the color of the sky?",
    'options':['Yellow','Green','Blue','Torquoise']
  },
  {'num':'3'
    ,'statement':"You speed up at yellow light?",
    'options':['True','False']
  },
  {'num':'4'
    ,'statement':"What is the color of the sky?",
    'options':['True','False']
  },
  {'num':'5'
    ,'statement':"What is the color of the sky?",
    'options':['True','False']
  },
  {'num':'6'
    ,'statement':"What is the color of the sky?",
    'options':['True','False']
  },
  {'num':'7'
    ,'statement':"What is the color of the sky?",
    'options':['True','False']
  }

  ]
  constructor(){

  }
}
