export default {
    curryLast: (f, args = []) => function fn(a) {
        args.push(a);
        if(args.length == f.length) {
            args.pop();
            return f(...args, a);
        }
        else 
            return fn;
    },
    stutter: (f, time, timer) => (...args) => {
        clearTimeout(timer.timer);
        timer.timer = setTimeout(f, time, args);
    }
}