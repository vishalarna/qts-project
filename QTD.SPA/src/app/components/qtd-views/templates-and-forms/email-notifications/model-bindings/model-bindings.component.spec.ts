import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModelBindingsComponent } from './model-bindings.component';

describe('ModelBindingsComponent', () => {
  let component: ModelBindingsComponent;
  let fixture: ComponentFixture<ModelBindingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModelBindingsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModelBindingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
