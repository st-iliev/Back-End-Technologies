import {lookupChar} from './charLookup.js'
import { expect } from 'chai'

describe('Test of charLookup function',()=>{
    it('should return undefind if first parameter is not a string type',()=> {
        // Arrange
        const secondParameter = 1;
        // Act
        let numberResult = lookupChar(3,secondParameter)
        let booleanResult = lookupChar(true,secondParameter)
        let nullResult = lookupChar(null,secondParameter)
        let floatResult = lookupChar(5.3,secondParameter)
        // Assert
        expect(numberResult).to.be.undefined
        expect(booleanResult).to.be.undefined
        expect(nullResult).to.be.undefined
        expect(floatResult).to.be.undefined
    })
    it('should return undefind if second parameter is not a number type',()=> {
        // Arrange
        const firstParameter = 'welcome';
        // Act
        let stringResult = lookupChar(firstParameter,'nice')
        let booleanResult = lookupChar(firstParameter,false)
        let nullResult = lookupChar(firstParameter,null)
        let floatResult = lookupChar(firstParameter,7.7)
        // Assert
        expect(stringResult).to.be.undefined
        expect(booleanResult).to.be.undefined
        expect(nullResult).to.be.undefined
        expect(floatResult).to.be.undefined
    })
    it('should return text Incorrect index if second parameter  is negative number', () =>{
        // Arrange
        const firstParameter = 'welcome';
        // Act
        let result = lookupChar(firstParameter,-3)   
        // Assert
        expect(result).to.equals('Incorrect index') 
    })
    it('should return text Incorrect index if second parameter is bigger than string lenght', () =>{
        // Arrange
        const firstParameter = 'welcome';
        // Act
        let result = lookupChar(firstParameter,20)   
        // Assert
        expect(result).to.equals('Incorrect index') 
    })
    it('should return text Incorrect index if second parameter is equal to string lenght', () =>{
        // Arrange
        const firstParameter = 'welcome';
        // Act
        let result = lookupChar(firstParameter,7)   
        // Assert
        expect(result).to.equals('Incorrect index') 
    })
    it('should return character at specific index if input data are correct', () =>{
        // Arrange
        const firstParameter = 'welcome';
        const secondParameter = 1
        // Act
        let result = lookupChar(firstParameter,secondParameter)
       
        // Assert
        expect(result).to.equals('e')
        
    })
})