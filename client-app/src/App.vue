<template>
  <div id="app" class="container">
    <header class="menu">
      <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <router-link to="/" class="navbar-brand">WordFinder</router-link>
        <button class="navbar-toggler" type="button" data-toggle="collapse"
          data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent"
          aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="navbarSupportedContent">
          <ul class="navbar-nav mr-auto">
            <li class="nav-item active">
              <router-link class="nav-link" :to="{name: 'supplyText'}">
                Find Words</router-link>
            </li>
            <li class="nav-item">
              <router-link class="nav-link" :to="{name: 'myWords'}">
                My Words</router-link>
            </li>
          </ul>
        </div>
      </nav>
    </header>
    
    <main class="currentWindow">
      <router-view></router-view>
    </main>
  </div>
</template>

<script>
  export default {
    name: 'app'
  }
</script>

<style lang="scss" scoped>
  .window {
    margin: 1em 0;
  }
</style>
