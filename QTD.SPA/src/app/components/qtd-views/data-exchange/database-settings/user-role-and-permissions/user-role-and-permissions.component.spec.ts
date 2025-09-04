import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserRoleAndPermissionsComponent } from './user-role-and-permissions.component';

describe('UserRoleAndPermissionsComponent', () => {
  let component: UserRoleAndPermissionsComponent;
  let fixture: ComponentFixture<UserRoleAndPermissionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserRoleAndPermissionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserRoleAndPermissionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
