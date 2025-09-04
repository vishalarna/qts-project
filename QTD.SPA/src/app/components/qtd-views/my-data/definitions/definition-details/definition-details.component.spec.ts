import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DefinitionDetailsComponent } from './definition-details.component';

describe('DefinitionDetailsComponent', () => {
  let component: DefinitionDetailsComponent;
  let fixture: ComponentFixture<DefinitionDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DefinitionDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DefinitionDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
