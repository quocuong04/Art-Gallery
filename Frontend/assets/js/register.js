// Show password
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
    
    showHiddenPass('register-pass','register-eye');

//Show confirm password
document.getElementById('confirm-eye').addEventListener('click', function() {
  var confirmPassInput = document.getElementById('register-confirm-pass');
  var confirmPassEyeIcon = document.getElementById('confirm-eye');

  if (confirmPassInput.type === 'password') {
     confirmPassInput.type = 'text';
     confirmPassEyeIcon.classList.remove('ri-eye-off-line');
     confirmPassEyeIcon.classList.add('ri-eye-line');
  } else {
     confirmPassInput.type = 'password';
     confirmPassEyeIcon.classList.remove('ri-eye-line');
     confirmPassEyeIcon.classList.add('ri-eye-off-line');
  }
});

//Check password and confirm password
document.querySelector('.register__form').addEventListener('submit', function(event) {
  var pass1 = document.getElementById('register-pass').value;
  var pass2 = document.getElementById('register-confirm-pass').value;
  
  if (pass1 !== pass2) {
     alert('Passwords do not match!');
     event.preventDefault();
  }
});
