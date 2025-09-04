import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TemplatesAndFormsComponent } from './templates-and-forms.component';

describe('TemplatesAndFormsComponent', () => {
  let component: TemplatesAndFormsComponent;
  let fixture: ComponentFixture<TemplatesAndFormsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TemplatesAndFormsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TemplatesAndFormsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
