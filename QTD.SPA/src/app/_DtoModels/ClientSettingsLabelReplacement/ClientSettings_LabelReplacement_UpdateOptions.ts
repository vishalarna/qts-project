export class ClientSettings_LabelReplacement_UpdateOptions {

  labelReplacements: LabelReplacementOptions[];

  UpdateLabelReplacement = (defaultLabel: string, labelReplacement: string) => {

    if (!this.labelReplacements) this.labelReplacements = [];

    let labelReplacementOption = this.labelReplacements.filter(r => r.defaultLabel === defaultLabel)[0];

    if (!labelReplacementOption) {
      labelReplacementOption = new LabelReplacementOptions(defaultLabel, labelReplacement);
      this.labelReplacements.push(labelReplacementOption);
    } else {
      labelReplacementOption.SetLabelReplacementvalue(labelReplacement);
    }
  }
}

export class LabelReplacementOptions {
  defaultLabel: string;
  labelReplacement: string;

  constructor(defaultLabel, labelReplacement) {
    this.labelReplacement = labelReplacement;
    this.defaultLabel = defaultLabel;
  }

  SetLabelReplacementvalue = (labelReplacement) => {
    this.labelReplacement = labelReplacement;
  }
}
