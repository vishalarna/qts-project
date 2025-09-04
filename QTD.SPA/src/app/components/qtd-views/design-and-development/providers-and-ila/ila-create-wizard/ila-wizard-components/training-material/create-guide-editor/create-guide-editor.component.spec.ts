import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateGuideEditorComponent } from './create-guide-editor.component';

describe('CreateGuideEditorComponent', () => {
  let component: CreateGuideEditorComponent;
  let fixture: ComponentFixture<CreateGuideEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateGuideEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateGuideEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
