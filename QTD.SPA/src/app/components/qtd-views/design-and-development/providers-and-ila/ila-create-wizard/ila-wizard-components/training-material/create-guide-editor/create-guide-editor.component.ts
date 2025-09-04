import { Component, OnInit } from '@angular/core';
import * as ckcustomBuild from 'src/app/ckcustomBuild/build/ckeditor.js';

@Component({
  selector: 'app-create-guide-editor',
  templateUrl: './create-guide-editor.component.html',
  styleUrls: ['./create-guide-editor.component.scss']
})
export class CreateGuideEditorComponent implements OnInit {
  public Editor = ckcustomBuild
  public configCKEditor = {
    placeholder:'Start typing here....'
  }
  constructor(

  ) { }

  ngOnInit(): void {
  }

}
