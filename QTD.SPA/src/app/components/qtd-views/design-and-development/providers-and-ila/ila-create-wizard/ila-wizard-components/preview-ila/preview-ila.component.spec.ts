import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PreviewIlaComponent } from './preview-ila.component';

describe('PreviewIlaComponent', () => {
  let component: PreviewIlaComponent;
  let fixture: ComponentFixture<PreviewIlaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PreviewIlaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PreviewIlaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
