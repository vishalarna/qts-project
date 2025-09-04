export const clientSettingsTestData = [
  {
    id: 1,
    name: 'Class Schedule',
    enabled: false,
    steps: [{
      template: 'Hello %FirstName% %LastName% from the Class Schedule',
      order: 1,
      model: [
        {
          name: 'First Name',
          template: '%FirstName%'
        },
        {
          name: 'Last Name',
          template: '%LastName%'
        },
      ],
      customSettings: [{
        key: "ScheduleForNext",
        value: "20"
      },
        {
          key: "EmailFrequency",
          value: "weekly"
        }]
    }],
    customSettings: []
  },
  {
    id: 2,
    name: 'Certification Expiration',
    enabled: false,
    steps: [
      {
        template: 'Hello %FirstName% %LastName% from Cert Expiration 1',
        order: 1,
        model: [
          {
            name: 'First Name',
            template: '%FirstName%'
          },
          {
            name: 'Last Name',
            template: '%LastName%'
          },
        ],
        customSettings: [{
          key: "ExpiringWithin",
          value: "20"
        },
          {
            key: "EmailFrequency",
            value: "monthly"
          },
          {
            key: "SendToEmployees",
            value: "false"
          },
          {
            key: "SendToManagers",
            value: "true"
          },
          {
            key: "CustomEmailAddresses",
            value: "nickolas@qualitytrainingsystems, someone1@qualitytrainingsystems"
          },
        ]
      },
      {
        template: 'Hello %FirstName% %LastName% from the Cert Expiration 2',
        order: 2,
        model: [
          {
            name: 'First Name',
            template: '%FirstName%'
          },
          {
            name: 'Last Name',
            template: '%LastName%'
          },
        ],
        customSettings: [{
          key: "ExpiringWithin",
          value: "20"
        },
          {
            key: "EmailFrequency",
            value: "monthly"
          },
          {
            key: "SendToEmployees",
            value: "false"
          },
          {
            key: "SendToManagers",
            value: "true"
          },
          {
            key: "CustomEmailAddresses",
            value: "nickolas@qualitytrainingsystems, someone2@qualitytrainingsystems"
          }]
      },
      {
        template: 'Hello %FirstName% %LastName% from Cert Expiration 3',
        order: 3,
        model: [
          {
            name: 'First Name',
            template: '%FirstName%'
          },
          {
            name: 'Last Name',
            template: '%LastName%'
          },
        ],
        customSettings: [{
          key: "ExpiringWithin",
          value: "20"
        },
          {
            key: "EmailFrequency",
            value: "monthly"
          },
          {
            key: "SendToEmployees",
            value: "false"
          },
          {
            key: "SendToManagers",
            value: "true"
          },
          {
            key: "CustomEmailAddresses",
            value: "nickolas@qualitytrainingsystems, someone3@qualitytrainingsystems"
          }]
      }
    ],
    customSettings: []
  },
  {
    id: 3,
    name: 'EMP Login',
    enabled: false,
    steps: [{
      template: 'Hello %FirstName% %LastName% from the EMP Login',
      order: 1,
      model: [
        {
          name: 'First Name',
          template: '%FirstName%'
        },
        {
          name: 'Last Name',
          template: '%LastName%'
        },
      ],
      customSettings: []
    }],
    customSettings: []
  },
  {
    id: 4,
    name: 'EMP Test',
    enabled: false,
    steps: [{
      template: 'Hello %FirstName% %LastName% from the EMP Login',
      order: 1,
      model: [
        {
          name: 'First Name',
          template: '%FirstName%'
        },
        {
          name: 'Last Name',
          template: '%LastName%'
        },
      ],
      CustomSettings: []
    }]
  }];

