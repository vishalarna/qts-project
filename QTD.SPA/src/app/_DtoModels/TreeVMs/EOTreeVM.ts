export class EOCatTreeVM{
  id!:any;
  number!:any;
  title!:string;
  active!:boolean;
  enablingObjective_SubCategories:EOSubCatTreeVM[] = []
}

export class EOSubCatTreeVM{
  id!:any;
  number!:any;
  title!:string;
  active!:boolean;
  enablingObjective_Topics:EOTopicTreeVM[] = [];
  enablingObjectives!:EOTreeVM[];
}

export class EOTreeVM{
  id!:any;
  number!:any;
  description!:string;
  active!:boolean;
  isMetaEO!:boolean;
  isSkillQualification!:boolean;
}

export class EOTopicTreeVM{
  id!:any;
  number!:any;
  active!:boolean;
  title!:string;
  enablingObjectives:EOTreeVM[] = [];
}
