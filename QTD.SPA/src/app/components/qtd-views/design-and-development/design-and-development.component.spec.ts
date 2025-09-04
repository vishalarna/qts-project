import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DesignAndDevelopmentComponent } from './design-and-development.component';

describe('DesignAndDevelopmentComponent', () => {
  let component: DesignAndDevelopmentComponent;
  let fixture: ComponentFixture<DesignAndDevelopmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DesignAndDevelopmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DesignAndDevelopmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
