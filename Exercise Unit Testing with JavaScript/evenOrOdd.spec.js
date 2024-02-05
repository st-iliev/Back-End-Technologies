import { isOddOrEven } from './evenOrOdd.js'
import { expect } from 'chai'

describe('Test of function even or odd', () => {
    it('should return undefined if is given a invalid input', () => {
        // Arrange
        const number = 666;
        const boolean = false;
        // Act
        let numberResult = isOddOrEven(number)
        let booleanResult = isOddOrEven(boolean) 
        // Assert
        expect(numberResult).to.equals(undefined)
        expect(booleanResult).to.equals(undefined)
    })
    it('should return text odd if given string has odd lenght', ()=>{
        // Arrange
        const input = 'asdf3';
        // Act
        let result = isOddOrEven(input)
        // Assert
        expect(result).to.equals('odd')
    })
    it('should return text even if  given string has even lenght', ()=>{
        // Arrange
        const input = 'as';
        // Act
        let result = isOddOrEven(input)
        // Assert
      
        expect(result).to.equals('even')
    })
})