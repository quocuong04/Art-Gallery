/*=============== SHOW HIDDEN - PASSWORD ===============*/
const showHiddenPass = (registerPass, registerEye) =>{
    const input = document.getElementById(registerPass),
          iconEye = document.getElementById(registerEye)
    
    iconEye.addEventListener('click', () =>{
       // Change password to text
       if(input.type === 'password'){
          // Switch to text
          input.type = 'text'
    
          // Icon change
          iconEye.classList.add('ri-eye-line')
          iconEye.classList.remove('ri-eye-off-line')
       } else{
          // Change to password
          input.type = 'password'
    
          // Icon change
          iconEye.classList.remove('ri-eye-line')
          iconEye.classList.add('ri-eye-off-line')
       }
    })
    }
    
    showHiddenPass('register-pass','register-eye')
    