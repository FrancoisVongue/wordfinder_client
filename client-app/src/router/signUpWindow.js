import signUpWindow from '../components/signUpWindow/signUpWindow'

export default { 
name: 'signUp',
  path: '/sign-up',
  component: signUpWindow,
  props: (route) => ({currentWindow: route.name}),
}