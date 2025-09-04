const _ = require('lodash');
export const pascalToCamel = (object) => {
    const tempObject: Array<any> = [];
    if(Array.from(object).length > 1){
      for(const value of Array.from(object)){
        const tempObj =  camelizeKeys(value);
        tempObject.push(tempObj);
      }
      return tempObject;
    } else {
      const tempObj =  camelizeKeys(object);
      return tempObj;
    }
   
  };
  
  const camelizeKeys = (obj) => {
    if (Array.isArray(obj)) {
      return obj.map(v => camelizeKeys(v));
    } else if (obj != null && obj.constructor === Object) {
      return Object.keys(obj).reduce(
        (result, key) => ({
          ...result,
          [_.camelCase(key)]: camelizeKeys(obj[key]),
        }),
        {},
      );
    }
    return obj;
  };